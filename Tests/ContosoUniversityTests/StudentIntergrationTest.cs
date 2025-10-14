using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.StudentViewModels;
using ContosoUniversity.Pages.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Xunit;

public class StudentIntegrationTests
{
    private SchoolContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SchoolContext>()
            .UseInMemoryDatabase("IntegrationTestDb")
            .Options;

        return new SchoolContext(options);
    }

    [Fact]
    public async Task OnPostAsync_CreatesStudent_AndRedirects()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var logger = NullLogger<CreateModel>.Instance;


        var pageModel = new CreateModel(context, logger)
        {
            StudentVM = new StudentVM
            {
                FirstName = "John",
                LastName = "Doe",
                EnrollmentDate = DateTime.Today
            }
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        var savedStudent = await context.Students.FirstOrDefaultAsync(s => s.LastName == "Doe");

        Assert.NotNull(savedStudent);
        Assert.Equal("John", savedStudent.FirstName);
        Assert.IsType<RedirectToPageResult>(result);
    }
}
