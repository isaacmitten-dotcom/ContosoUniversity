using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Logging;

namespace ContosoUniversity.Pages.Students
{
    public class DeleteModel(ContosoUniversity.Data.SchoolContext context, ILogger<DeleteModel> logger) : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context = context;
        private readonly ILogger<DeleteModel> _logger = logger;

        [BindProperty]
        public Student? Student { get; set; }
        public string? ErrorMessage;


        public async Task<IActionResult> OnGetAsync(int? id, bool saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (Student == null) return NotFound();


            if (saveChangesError) {
                ErrorMessage = $"Delete {id} failed";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);


            try {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Student deleted successfully";


                return RedirectToPage("./Index");

            }
            catch (DbUpdateException ex) {
                _logger.LogError(ex, $"Delete failed for {id}");
                return RedirectToPage("./Delete", new {id, saveChangesError = true});
            }
        }
    }
}
