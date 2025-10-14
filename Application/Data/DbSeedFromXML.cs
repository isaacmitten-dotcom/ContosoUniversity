using ContosoUniversity.Models;
using System.Xml.Linq;

namespace ContosoUniversity.Data
{
    public class DbSeedFromXML
    {

        public static void SeedFromXml(SchoolContext context, string xmlPath)
        {
            Console.WriteLine("Starting seeding from XML...");


            var doc = XDocument.Load(xmlPath);
            var root = doc.Element("ContosoUniversityData");

            var studentElements = root.Element("Students")?.Elements("Student") ?? Enumerable.Empty<XElement>();
            foreach (var el in studentElements)
            {
                var first = el.Attribute("FirstName")?.Value;
                var last = el.Attribute("LastName")?.Value;
                var date = DateTime.Parse(el.Attribute("EnrollmentDate")?.Value);

                if (!context.Students.Any(s => s.FirstName == first && s.LastName == last))
                {
                    context.Students.Add(new Student
                    {
                        FirstName = first,
                        LastName = last,
                        EnrollmentDate = date
                    });
                }
            }

            var instructorElements = root.Element("Instructors")?.Elements("Instructor") ?? Enumerable.Empty<XElement>();
            foreach (var el in instructorElements)
            {
                var first = el.Attribute("FirstName")?.Value;
                var last = el.Attribute("LastName")?.Value;
                var hireDate = DateTime.Parse(el.Attribute("HireDate")?.Value);

                if (!context.Instructors.Any(i => i.FirstName == first && i.LastName == last))
                {
                    context.Instructors.Add(new Instructor
                    {
                        FirstName = first,
                        LastName = last,
                        HireDate = hireDate
                    });
                }
            }

            context.SaveChanges(); 

            var instructors = context.Instructors.ToList();
            var students = context.Students.ToList();


            var officeElements = root.Element("OfficeAssignments")?.Elements("OfficeAssignment") ?? Enumerable.Empty<XElement>();
            foreach (var el in officeElements)
            {
                var last = el.Attribute("InstructorLastName")?.Value;
                var loc = el.Attribute("Location")?.Value;

                var instructor = instructors.FirstOrDefault(i => i.LastName == last);
                if (instructor != null && !context.OfficeAssignments.Any(o => o.InstructorID == instructor.ID))
                {
                    context.OfficeAssignments.Add(new OfficeAssignment
                    {
                        InstructorID = instructor.ID,
                        Location = loc
                    });
                }
            }

            context.SaveChanges();


            var departmentElements = root.Element("Departments")?.Elements("Department") ?? Enumerable.Empty<XElement>();
            foreach (var el in departmentElements)
            {
                var name = el.Attribute("Name")?.Value;
                var budget = decimal.Parse(el.Attribute("Budget")?.Value);
                var startDate = DateTime.Parse(el.Attribute("StartDate")?.Value);
                var adminLast = el.Attribute("AdministratorLastName")?.Value;

                var admin = instructors.FirstOrDefault(i => i.LastName == adminLast);

                if (!context.Departments.Any(d => d.Name == name))
                {
                    context.Departments.Add(new Department
                    {
                        Name = name,
                        Budget = budget,
                        StartDate = startDate,
                        Administrator = admin
                    });
                }
            }

            context.SaveChanges();

            var departments = context.Departments.ToList();


            var courseElements = root.Element("Courses")?.Elements("Course") ?? Enumerable.Empty<XElement>();
            foreach (var el in courseElements)
            {
                var courseID = int.Parse(el.Attribute("CourseID")?.Value);
                var title = el.Attribute("Title")?.Value;
                var credits = int.Parse(el.Attribute("Credits")?.Value);
                var deptName = el.Attribute("Department")?.Value;

                var dept = departments.FirstOrDefault(d => d.Name == deptName);
                var instructorLNs = el.Element("Instructors")?.Elements("InstructorLastName").Select(x => x.Value).ToList() ?? new List<string>();
                var courseExists = context.Courses.Any(c => c.CourseID == courseID);

                if (!courseExists)
                {
                    var course = new Course
                    {
                        CourseID = courseID,
                        Title = title,
                        Credits = credits,
                        Department = dept,
                        Instructors = instructors.Where(i => instructorLNs.Contains(i.LastName)).ToList()
                    };

                    context.Courses.Add(course);
                }
            }

            context.SaveChanges();
            var courses = context.Courses.ToList();


            var enrollmentElements = root.Element("Enrollments")?.Elements("Enrollment") ?? Enumerable.Empty<XElement>();
            foreach (var el in enrollmentElements)
            {
                var studentLast = el.Attribute("StudentLastName")?.Value;
                var courseTitle = el.Attribute("CourseTitle")?.Value;
                var gradeAttr = el.Attribute("Grade")?.Value;

                var student = students.FirstOrDefault(s => s.LastName == studentLast);
                var course = courses.FirstOrDefault(c => c.Title == courseTitle);

                if (student != null && course != null && !context.Enrollments.Any(e => e.StudentID == student.Id && e.CourseID == course.CourseID))
                {
                    Grade? grade = null;
                    if (!string.IsNullOrEmpty(gradeAttr) && Enum.TryParse<Grade>(gradeAttr, out var g))
                        grade = g;

                    context.Enrollments.Add(new Enrollment
                    {
                        StudentID = student.Id,
                        CourseID = course.CourseID,
                        Grade = grade
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
