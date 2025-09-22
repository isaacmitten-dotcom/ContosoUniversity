# ContosoUniversity

## Test Plan

This test plan covers functional testing for the Students CRUD operations, validation, sorting, filtering, paging, PRG success messaging.

###  Create Student

- Navigate to /Students/Create.
- Submit form with valid FirstName, LastName, and EnrollmentDate.
- Confirm redirection to Index page.
- Confirm success message is displayed via TempData.
- Submit form with empty required fields (e.g., FirstName).

###  Edit Student

- Navigate to /Students/Edit/{id} for an existing student.
- Confirm form is pre-filled with student data.
- Update fields with valid values and submit.
- Confirm redirection to Index page and success message appears.
- Submit invalid data (e.g., empty LastName).
- Confirm validation messages appear and data is not saved.
- Confirm invalid ID results in 404.

### Delete Student

- Navigate to /Students/Delete/{id}.
- Confirm student details are displayed for confirmation.
- Confirm deletion and verify student is removed.
- Confirm redirection to Index page with success message.
- Simulate database error and confirm error message is displayed after redirection.

### Sorting and Filtering

- Visit /Students/Index.
- Click on column headers (Last Name, Enrollment Date) to test ascending and descending sorting.
- Use search box to filter by first or last name.
- Confirm that filtering narrows the result set correctly.
- Confirm that sorting and filtering state is preserved when paginating.

### Paging

- Confirm only a limited number of records are shown per page (as defined in appsettings).
- Use "Next" and "Previous" buttons to navigate pages.
- Confirm correct records are displayed on each page.
- Attempt to access a page index beyond the available range and confirm fallback behavior.

### Validation

- Confirm that [Required] fields are enforced.
- Enter invalid or missing values and confirm validation messages appear.
- Test both client-side and server-side validation scenarios.
- Ensure invalid submissions do not save or redirect.

### PRG and TempData

- After a successful Create, Edit, or Delete, confirm that a redirect occurs instead of returning the view directly.
- Confirm that TempData is used to store and display a one-time success message after redirection.
- Refresh the page after redirection and verify the message does not reappear.

