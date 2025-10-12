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
using System.ComponentModel.DataAnnotations;


namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext? _context;
        private readonly ILogger<CreateModel>? _logger;


        public CreateModel(ContosoUniversity.Data.SchoolContext context, ILogger<CreateModel>? logger)
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
        public SchoolContext Context { get; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state when creating student. Errors: {@ModelErrors}", ModelState.Values.SelectMany(v => v.Errors));
                return Page();
            }


            var entry = _context.Students.Add(new Student());

            entry.CurrentValues.SetValues(StudentVM);

            _logger.LogInformation("Creating student with data: {@StudentVM}", StudentVM);


            await _context.SaveChangesAsync();

           // This was retrning a null reference for the integration test
           // TempData["Message"] = "Student created successfully";
            return RedirectToPage("./Index");
        }
    }
}
