# ContosoUniversity
## XML Import/Export

This project supports importing and exporting data using an XML file.

The file is located at: Data/ContosoUniversityData.xml

### XML Schema

<ContosoUniversityData>
  <Students>
    <Student FirstName="..." LastName="..." EnrollmentDate="YYYY-MM-DD" />
  </Students>
  <Instructors>
    <Instructor FirstName="..." LastName="..." HireDate="YYYY-MM-DD" />
  </Instructors>
  <OfficeAssignments>
    <OfficeAssignment InstructorLastName="..." Location="..." />
  </OfficeAssignments>
  <Departments>
    <Department Name="..." Budget="..." StartDate="YYYY-MM-DD" AdministratorLastName="..." />
  </Departments>
  <Courses>
    <Course CourseID="..." Title="..." Credits="..." Department="...">
      <Instructors>
        <InstructorLastName>...</InstructorLastName>
      </Instructors>
    </Course>
  </Courses>
  <Enrollments>
    <Enrollment StudentLastName="..." CourseTitle="..." Grade="..." />
  </Enrollments>
</ContosoUniversityData>
