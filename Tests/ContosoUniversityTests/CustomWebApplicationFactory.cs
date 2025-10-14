using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove all existing DbContextOptions<SchoolContext> registrations
            var descriptors = services.Where(
                d => d.ServiceType == typeof(DbContextOptions<SchoolContext>)).ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            services.AddEntityFrameworkInMemoryDatabase();


            services.AddDbContext<SchoolContext>(options =>
            {
                options.UseInMemoryDatabase($"TestDb");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            db.Database.EnsureCreated();

            // Seed the database with test data
            if (!db.Students.Any())
            {
                db.Students.Add(new Student { LastName = "Smith", FirstName = "John", EnrollmentDate = DateTime.Now });
                db.SaveChanges();
                Console.WriteLine($"Seeded {db.Students.Count()} students");

            }

        });
    }
}
