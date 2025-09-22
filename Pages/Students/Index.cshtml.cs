using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

using Microsoft.IdentityModel.Tokens;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        public string? NameSort { get; set; }
        public string? DateSort { get; set; }  

        public string? CurrentFilter { get; set; }
        
        public string? CurrentSort {  get; set; }


        public PaginatedList<Student> Student { get; set; }
        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;


            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;


            IQueryable<Student> studentsIq = from s in _context.Student
                                                 select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                studentsIq = studentsIq.Where(s => s.LastName.Contains(searchString) ||
                                                   s.FirstName.Contains(searchString));
            }


            studentsIq = sortOrder switch
            {
                "name_desc" => studentsIq.OrderByDescending(s => s.LastName),
                "Date" => studentsIq.OrderBy(s => s.EnrollmentDate),
                "date_desc" => studentsIq.OrderByDescending(s => s.EnrollmentDate),
                 _ =>  studentsIq.OrderBy(s => s.LastName) 
            };



            var pageSize = _configuration.GetValue("PageSize", 4);
            Student = await PaginatedList<Student>.CreateAsync(studentsIq.AsNoTracking()
                                                             , pageIndex ?? 1
                                                             , pageSize); 


        }
    }
}
