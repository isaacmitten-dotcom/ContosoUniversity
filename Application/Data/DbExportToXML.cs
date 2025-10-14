using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbExportToXML
    {
        private readonly SchoolContext _context;

        public DbExportToXML(SchoolContext context)
        {
            _context = context;
        }

        public void Export(string filePath)
        {
            var students = _context.Students.ToList();
            var instructors = _context.Instructors.ToList();
            var officeAssignments = _context.OfficeAssignments.ToList();
            var departments = _context.Departments.ToList();
            var courses = _context.Courses
                .Select(c => new
                {
                    Course = c,
                    Department = c.Department,
                    Instructors = c.Instructors
                })
                .ToList();
            var enrollments = _context.Enrollments.ToList();

            var xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("ContosoUniversityData",
                    new XElement("Students",
                        students.Select(s =>
                            new XElement("Student",
                                new XAttribute("FirstName", s.FirstName ?? ""),
                                new XAttribute("LastName", s.LastName ?? ""),
                                new XAttribute("EnrollmentDate", s.EnrollmentDate.ToString("yyyy-MM-dd"))
                            )
                        )
                    ),
                    new XElement("Instructors",
                        instructors.Select(i =>
                            new XElement("Instructor",
                                new XAttribute("FirstName", i.FirstName ?? ""),
                                new XAttribute("LastName", i.LastName ?? ""),
                                new XAttribute("HireDate", i.HireDate.ToString("yyyy-MM-dd"))
                            )
                        )
                    ),
                    new XElement("OfficeAssignments",
                        officeAssignments.Select(o =>
                            new XElement("OfficeAssignment",
                                new XAttribute("InstructorLastName", o.Instructor?.LastName ?? ""),
                                new XAttribute("Location", o.Location ?? "")
                            )
                        )
                    ),
                    new XElement("Departments",
                        departments.Select(d =>
                            new XElement("Department",
                                new XAttribute("Name", d.Name ?? ""),
                                new XAttribute("Budget", d.Budget),
                                new XAttribute("StartDate", d.StartDate.ToString("yyyy-MM-dd")),
                                new XAttribute("AdministratorLastName", d.Administrator?.LastName ?? "")
                            )
                        )
                    ),
                    new XElement("Courses",
                        courses.Select(c =>
                            new XElement("Course",
                                new XAttribute("CourseID", c.Course.CourseID),
                                new XAttribute("Title", c.Course.Title ?? ""),
                                new XAttribute("Credits", c.Course.Credits),
                                new XAttribute("Department", c.Department?.Name ?? ""),
                                new XElement("Instructors",
                                    c.Instructors?.Select(i =>
                                        new XElement("InstructorLastName", i.LastName ?? "")
                                    ) ?? Enumerable.Empty<XElement>()
                                )
                            )
                        )
                    ),
                    new XElement("Enrollments",
                        enrollments.Select(e =>
                            new XElement("Enrollment",
                                new XAttribute("StudentLastName", e.Student?.LastName ?? ""),
                                new XAttribute("CourseTitle", e.Course?.Title ?? ""),
                                e.Grade != null
                                    ? new XAttribute("Grade", e.Grade)
                                    : null
                            )
                        )
                    )
                )
            );

            xml.Save(filePath);
        }
    }
}
