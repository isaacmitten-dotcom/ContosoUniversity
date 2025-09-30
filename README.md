# ContosoUniversity

### XML Schema

  ```
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
```
### Partial Log
```
Starting seeding from XML...
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (30ms) [Parameters=[@__first_0='?' (Size = 50), @__last_1='?' (Size = 50)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Student] AS [s]
              WHERE [s].[FirstName] = @__first_0 AND [s].[LastName] = @__last_1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__first_0='?' (Size = 50), @__last_1='?' (Size = 50)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Student] AS [s]
              WHERE [s].[FirstName] = @__first_0 AND [s].[LastName] = @__last_1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__first_0='?' (Size = 50), @__last_1='?' (Size = 50)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Student] AS [s]
              WHERE [s].[FirstName] = @__first_0 AND [s].[LastName] = @__last_1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[@__first_0='?' (Size = 50), @__last_1='?' (Size = 50)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Student] AS [s]
              WHERE [s].[FirstName] = @__first_0 AND [s].[LastName] = @__last_1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[@__first_0='?' (Size = 50), @__last_1='?' (Size = 50)], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM [Student] AS [s]
              WHERE [s].[FirstName] = @__first_0 AND [s].[LastName] = @__last_1) THEN CAST(1 AS bit)
          ELSE CAST(0 AS bit)
      END
```
