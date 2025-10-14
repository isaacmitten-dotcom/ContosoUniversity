using Xunit;
using ContosoUniversity.Models;

public class EnrollmentModelTests
{

    [Fact]
    public void EnrollmentGradeCanBeAssignedValidEnumValue()
    {
        var enrollment = new Enrollment
        {
            EnrollmentID = 1,
            StudentID = 100,
            CourseID = 200,
            Grade = Grade.B
        };

        Assert.Equal(Grade.B, enrollment.Grade);
    }

    [Fact]
    public void EnrollmentGradeCanBeNull()
    {
        var enrollment = new Enrollment
        {
            EnrollmentID = 2,
            StudentID = 101,
            CourseID = 201,
            Grade = null
        };

        Assert.Null(enrollment.Grade);
    }
}
