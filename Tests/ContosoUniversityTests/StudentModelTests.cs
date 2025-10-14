using System.ComponentModel.DataAnnotations;
using ContosoUniversity.Models;
using Xunit;

public class StudentModelTests
{
    [Fact]
    public void StudentWithMissingRequiredFieldsShouldBeInvalid()
    {
        var student = new Student(); 

        var context = new ValidationContext(student);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(student, context, results, true);

        Assert.False(isValid);

        var errorMessages = results.Select(r => r.ErrorMessage).ToList();

        Assert.Contains("The Last Name field is required.", errorMessages);
        Assert.Contains("The First Name field is required.", errorMessages);
    }

}
