using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models.Validation
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        public NotFutureDateAttribute() => ErrorMessage = "Enrollment date can not be in the future";

        public override bool IsValid(object? value)
        {
           if (value == null) return true;

           if (value is DateTime dt) return dt.Date <= DateTime.UtcNow.Date;
            
            return false;
        }
    }
}
