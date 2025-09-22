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

            public async Task<IActionResult> OnGetAsync(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Student = await _context.Student.FindAsync(id);

                if (Student == null)
                {
                    return NotFound();
                }
                return Page();
            }

            public async Task<IActionResult> OnPostAsync(int id)
            {
                var studentToUpdate = await _context.Student.FindAsync(id);

                if (studentToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Student>(
                    studentToUpdate,
                    //Had to change this prefix so that it binds correctly
                    "",
                    s => s.FirstName, s => s.LastName, s => s.EnrollmentDate))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }

                return Page();
            }
        }
    }

