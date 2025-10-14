using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages
{
    public class AboutModel : PageModel
    {

        public readonly SchoolContext _context;

        public AboutModel(SchoolContext context) => _context = context;

        public IList<EnrollmentDateGroup> Students { get; set; } 

        public async Task OnGetAsync()
        {
            IQueryable<EnrollmentDateGroup> data = from s in _context.Students
                                                   group s by s.EnrollmentDate into g
                                                   select new EnrollmentDateGroup
                                                   {
                                                       EnrollmentDate = g.Key,
                                                       StudentCount = g.Count()
                                                   };

            Students = await data.AsNoTracking().ToListAsync();
        }
    }
}
