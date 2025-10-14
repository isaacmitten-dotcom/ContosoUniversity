using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models.Validation;

namespace ContosoUniversity.Models.StudentViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? LastName { get; set; }


        [Required, StringLength(50)]
        public string? FirstName { get; set; }


        [DataType(DataType.Date)]
        [NotFutureDate]
        public DateTime EnrollmentDate { get; set; }
    }
}
