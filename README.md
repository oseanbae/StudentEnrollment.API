# Student Enrollment API

---

## 1. Project Overview

**Project Name:** StudentEnrollmentApi

**Problem Statement:** Schools need a system to manage which students are enrolled in which courses.

**Target Users:** School administrators managing student and course records.

**Why This is Portfolio-Worthy:**
- Demonstrates real relational data modeling (many-to-many)
- Goes beyond basic CRUD with business logic
- Uses patterns hiring managers actually look for (DTOs, EF Core relationships, migrations)
- Simple enough to finish, complex enough to matter

---

## 2. Scope

**Core Features:**
- Manage students (CRUD)
- Manage courses (CRUD)
- Enroll a student in a course
- Remove an enrollment
- View all courses a student is enrolled in
- View all students in a course
- Prevent duplicate enrollments

**Out of Scope:**
- Authentication and login
- Student grades
- Course scheduling and capacity limits
- Payment
- Email notifications
- Soft deletes

**Constraints and Assumptions:**
- One student cannot enroll in the same course twice
- EnrollmentDate is set automatically on creation
- Deletions are permanent
- No authentication required — all endpoints are open
- Email must be unique per student

---

## 3. System Design

**Core User Flow:**
```
Admin creates Student
       ↓
Admin creates Course
       ↓
Admin enrolls Student in Course
       ↓
Admin views Student's enrolled courses
       ↓
Admin removes enrollment if needed
```

---

## Entities

### Student
| Field | Type | Notes |
| :--- | :--- | :--- |
| **Id** | long | Primary key |
| **Name** | string | Required, max 100 chars |
| **Email** | string | Required, unique, valid email |
| **Enrollments** | ICollection\<Enrollment\> | Navigation property |

### Course
| Field | Type | Notes |
| :--- | :--- | :--- |
| **Id** | long | Primary key |
| **Title** | string | Required, max 100 chars |
| **Credits** | int | Required, range 1–6 |
| **Enrollments** | ICollection\<Enrollment\> | Navigation property |

### Enrollment
| Field | Type | Notes |
| :--- | :--- | :--- |
| **Id** | long | Primary key |
| **StudentId** | long | Foreign key → Student |
| **CourseId** | long | Foreign key → Course |
| **EnrollmentDate** | DateTime | Auto-set on creation |
| **Student** | Student | Navigation property |
| **Course** | Course | Navigation property |

---

### Pro-Tip for your Code:
When you get to **Step 2** and **Step 3**, remember to use the `[EmailAddress]` attribute on the `Email` field in your `Student` model. It’s a built-in .NET validator that saves you from writing complex regex yourself!

**API Endpoints**

| Method | Route | Purpose |
|---|---|---|
| GET | /api/students | Get all students |
| GET | /api/students/{id} | Get one student |
| POST | /api/students | Create student |
| PUT | /api/students/{id} | Update student |
| DELETE | /api/students/{id} | Delete student |
| GET | /api/courses | Get all courses |
| GET | /api/courses/{id} | Get one course |
| POST | /api/courses | Create course |
| PUT | /api/courses/{id} | Update course |
| DELETE | /api/courses/{id} | Delete course |
| GET | /api/enrollments | Get all enrollments |
| POST | /api/enrollments | Enroll student in course |
| DELETE | /api/enrollments/{id} | Remove enrollment |
| GET | /api/students/{id}/courses | Get all courses of a student |
| GET | /api/courses/{id}/students | Get all students in a course |

---

**Project Structure**
```
StudentEnrollmentApi/
├── Controllers/
│   ├── StudentsController.cs
│   ├── CoursesController.cs
│   └── EnrollmentsController.cs
├── Models/
│   ├── Student.cs
│   ├── Course.cs
│   └── Enrollment.cs
├── DTOs/
│   ├── StudentDTO.cs
│   ├── CourseDTO.cs
│   └── EnrollmentDTO.cs
├── Data/
│   └── AppDbContext.cs
├── appsettings.json
└── Program.cs
```

---

## 4. Build Phases

---

### Phase 1 — Project Foundation
**What You're Building:**
Project setup, real database connection, Student and Course models, first migration.

**Concepts and Skills:**
- Swapping InMemory for SQLite
- Connection strings in appsettings.json
- Data annotation attributes (`[Required]`, `[MaxLength]`, `[Range]`, `[EmailAddress]`)
- DbContext with multiple DbSets
- Migrations (`Add-Migration`, `Update-Database`)
- Reading generated migration files

**Deliverables:**
- Project created
- SQL Server connected
- Student.cs and Course.cs with validation attributes
- AppDbContext.cs with Students and Courses DbSets
- First migration applied and database created

**Definition of Done:**
```
✓ App runs without errors
✓ Database created in LocalDB
✓ Migration file exists and is readable
✓ Connection string is in appsettings.json not hardcoded
```

---

### Phase 2 — Student and Course CRUD
**What You're Building:**
Full CRUD for Students and Courses with proper DTOs.

**Concepts and Skills:**
- Separate CreateDTO vs ReadDTO
- Why Id doesn't belong in a CreateDTO
- Validation on DTOs not entities
- Same CRUD pattern from Todo API applied twice
- Testing endpoints in Swagger

**Deliverables:**
- StudentDTO.cs (ReadDTO + CreateDTO)
- CourseDTO.cs (ReadDTO + CreateDTO)
- StudentsController.cs with full CRUD
- CoursesController.cs with full CRUD
- All endpoints tested in Swagger

**Definition of Done:**
```
✓ All Student endpoints return correct status codes
✓ All Course endpoints return correct status codes
✓ No entity returned directly — always a DTO
✓ Invalid Id returns 404
✓ Validation errors return 400 automatically
```

---

### Phase 3 — Enrollment Model and Basic Endpoints
**What You're Building:**
Enrollment entity, bridge table, POST and DELETE enrollment endpoints.

**Concepts and Skills:**
- Many-to-many relationships via bridge table
- Foreign keys and navigation properties on both sides
- `OnModelCreating` for unique index (prevents duplicates at DB level)
- Second migration
- Business logic before insert (validate student exists, validate course exists, check duplicate)
- 409 Conflict response

**Deliverables:**
- Enrollment.cs with navigation properties
- AppDbContext updated with Enrollments DbSet and unique index
- Second migration applied
- EnrollmentDTO.cs
- EnrollmentsController with POST and DELETE
- Duplicate enrollment returns 409

**Definition of Done:**
```
✓ Enrollment is created with correct StudentId and CourseId
✓ EnrollmentDate is set automatically
✓ Invalid StudentId or CourseId returns 404
✓ Duplicate enrollment returns 409 Conflict
✓ DELETE removes enrollment correctly
```

---

### Phase 4 — Relationship Queries
**What You're Building:**
GET all enrollments, GET courses for a student, GET students in a course.

**Concepts and Skills:**
- `Include()` for loading navigation properties
- `ThenInclude()` for nested navigation properties
- Why navigation properties are null without Include()
- Returning enriched DTOs (StudentName and CourseTitle inside EnrollmentDTO)
- Nested routes (`/students/{id}/courses`)
- LINQ filtering with `Where()`

**Deliverables:**
- GET /api/enrollments returns enriched EnrollmentDTO with names
- GET /api/students/{id}/courses returns all courses for a student
- GET /api/courses/{id}/students returns all students in a course

**Definition of Done:**
```
✓ EnrollmentDTO includes StudentName and CourseTitle
✓ Navigation properties are never null in responses
✓ GET /students/{id}/courses returns correct courses
✓ GET /courses/{id}/students returns correct students
✓ Non-existent id returns 404
```

---

### Phase 5 — Polish and Review
**What You're Building:**
Cleaning up, testing edge cases, reviewing everything you built.

**Concepts and Skills:**
- Edge case thinking
- Swagger documentation review
- Code consistency check
- Revisiting the commented Todo API to see how far you've come

**Deliverables:**
- All 15 endpoints tested with valid and invalid inputs
- No entity returned directly anywhere
- No hardcoded values
- Code is readable and consistent

**Definition of Done:**
```
✓ All 15 endpoints work correctly
✓ All error cases return meaningful responses
✓ Database persists after app restart
✓ No crashes on invalid input
✓ You can explain every line you wrote
```

---

## 5. Skills Checklist

**Foundational** — you must know these cold

| Skill | Introduced In |
|---|---|
| Controller-based API architecture | Todo API |
| Attribute routing | Todo API |
| HTTP verbs and status codes | Todo API |
| Action return types | Todo API |
| Dependency injection | Todo API |
| DbContext and DbSet | Todo API |
| async/await and Task\<T\> | Todo API |
| DTO vs Entity separation | Todo API |
| SaveChangesAsync and FindAsync | Todo API |
| Model binding | Todo API |
| Data annotations (`[Required]` etc.) | Phase 1 |
| Migrations | Phase 1 |
| Connection strings | Phase 1 |
| Separate CreateDTO vs ReadDTO | Phase 2 |
| Many-to-many relationships | Phase 3 |
| Navigation properties | Phase 3 |
| Foreign keys | Phase 3 |
| Business logic before insert | Phase 3 |
| `Include()` for related data | Phase 4 |
| LINQ `Where()` and `Select()` | Phase 4 |
| Nested routes | Phase 4 |
| Enriched DTOs | Phase 4 |

---

**Advanced** — learn these after this project

| Skill | Why It Comes Later |
|---|---|
| Authentication and JWT | Needs solid API foundation first |
| Service layer pattern | Premature for this scope |
| Repository pattern | Adds abstraction you don't need yet |
| Unit testing | Need stable code to test first |
| Pagination and filtering | Stretch goal after MVP |
| Docker and deployment | Infrastructure comes after code |
| Caching | Optimization — not needed yet |
| Logging | Good habit, not blocking |

---

Start Phase 1. Create the project. Write `Student.cs`.

To make your project a true **Full-Stack** application, we need to add a Phase 6 and Phase 7 to your plan. Since you are already in the .NET ecosystem, **Blazor WebAssembly** is the most professional and efficient choice—it allows you to share your `DTOs` between the API and the Frontend.

Here is the supplementary plan to integrate into your documentation:

---

## Phase 6 — Frontend Foundation (Blazor)
**What You're Building:**
A separate Blazor WebAssembly project that communicates with your API via `HttpClient`.

**Concepts and Skills:**
* **CORS (Cross-Origin Resource Sharing):** Enabling your API to accept requests from your frontend.
* **Dependency Injection:** Injecting `HttpClient` into components.
* **Razor Components:** Building reusable UI pieces (Buttons, Inputs, Tables).
* **Shared Class Libraries:** Moving your DTOs to a "Shared" project so both API and Frontend use the exact same code.

**Deliverables:**
* Blazor WebAssembly project added to the solution.
* `Program.cs` in API updated with `app.UseCors()`.
* A base layout with a Sidebar Navigation (Students, Courses, Enrollments).

---

## Phase 7 — Feature Implementation
**What You're Building:**
Interactive pages for managing the school records.

### Page 1: Student Manager
* **Table:** Displays all students fetched from `GET /api/students`.
* **Form:** A "New Student" modal that sends a `POST` request.
* **Delete:** A button on each row that calls `DELETE /api/students/{id}` and refreshes the list.

### Page 2: Course Catalog
* **Card View:** Each course shown as a card with Title and Credits.
* **Detail View:** Clicking a course shows the list of students currently enrolled (using `GET /api/courses/{id}/students`).

### Page 3: Enrollment Wizard (The "Money" Feature)
* **Form:** Two dropdowns (Searchable Student list + Searchable Course list).
* **Action:** Submit button that calls `POST /api/enrollments`.
* **Feedback:** Show a "Success" toast or a "Conflict" error if the student is already enrolled.



---

## Updated Project Structure
```text
StudentEnrollmentApp/
├── StudentEnrollmentApi/ (Your current project)
├── StudentEnrollment.Client/ (The new Blazor project)
│   ├── Pages/
│   │   ├── Students.razor
│   │   ├── Courses.razor
│   │   └── Enroll.razor
│   └── Services/
│       └── ApiService.cs
└── StudentEnrollment.Shared/ (New! Shared DTOs)
    ├── StudentDTO.cs
    ├── CourseDTO.cs
    └── EnrollmentDTO.cs
```

---

## Full-Stack Definition of Done
* [ ] **No Hardcoded Data:** All information comes from the API/Database.
* [ ] **Loading States:** Users see a spinner while waiting for the API.
* [ ] **Error Handling:** If the API returns a `409 Conflict`, the frontend displays a clear "Student is already enrolled" message.
* [ ] **Responsive Design:** The dashboard looks good on both desktop and mobile (using Bootstrap, which comes with Blazor).

### Pro-Tip: The "Shared" Library
This is the "secret sauce" of .NET Full-Stack. Instead of writing your `StudentDTO` twice (once in C# and once in JavaScript), you put it in a **Shared Class Library**. Both projects reference it. If you change a property name, both projects update instantly!

**Do you want to start by creating the Shared Library first, or do you want to finish the API logic entirely before touching the UI?**