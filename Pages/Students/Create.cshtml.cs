using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Logging;
using ContosoUniversity.Models.StudentViewModels;


namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;
        private readonly ILogger<IndexModel> _logger;


        public CreateModel(ContosoUniversity.Data.SchoolContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public StudentVM StudentVM { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           if (!ModelState.IsValid) return Page();


            var entry = _context.Students.Add(new Student());

            entry.CurrentValues.SetValues(StudentVM);

            _logger.LogInformation("StudentVM: {@StudentVM}", StudentVM);


            await _context.SaveChangesAsync();

            TempData["Message"] = "Student created successfully";
            return RedirectToPage("./Index");
        }
    }
}
