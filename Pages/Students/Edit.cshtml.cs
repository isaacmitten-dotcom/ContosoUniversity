    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using ContosoUniversity.Data;
    using ContosoUniversity.Models;
using ContosoUniversity.Models.StudentViewModels;

namespace ContosoUniversity.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;
        private readonly ILogger<IndexModel> _logger;


        public EditModel(ContosoUniversity.Data.SchoolContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }


        public Student? Student { get; set; } = default!;

        [BindProperty]
        public StudentVM? StudentVM { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            // Map Student to StudentVM
            StudentVM = new StudentVM
            {
                FirstName = Student.FirstName,
                LastName = Student.LastName,
                EnrollmentDate = Student.EnrollmentDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var studentToUpdate = await _context.Students.FindAsync(id);

            if (studentToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return Page();


            //if (await TryUpdateModelAsync(
            //    studentToUpdate,
            //    "",
            //    s => s.FirstName, s => s.LastName, s => s.EnrollmentDate))
            //{

            studentToUpdate.FirstName = StudentVM.FirstName;
            studentToUpdate.LastName = StudentVM.LastName;
            studentToUpdate.EnrollmentDate = StudentVM.EnrollmentDate;

            var entry = _context.Entry(studentToUpdate);

                entry.CurrentValues.SetValues(StudentVM);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Student updated successfully";

                return RedirectToPage("./Index");
            

            //foreach (var key in Request.Form.Keys)
            //{
            //    _logger.LogInformation("Form key: " + key);
            //}

            //foreach (var state in ModelState)
            //{
            //    foreach (var error in state.Value.Errors)
            //    {
            //        _logger.LogError($"ModelState error for {state.Key}: {error.ErrorMessage}");
            //    }
            //}

            //_logger.LogInformation("Skipped tryupdate");

            //return Page();
        }
    }
}

