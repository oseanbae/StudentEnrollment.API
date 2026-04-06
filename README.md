# 🎓 Student Enrollment API

A RESTful API for managing students, courses, and enrollments.
Built to demonstrate relational data modeling, clean architecture, and practical backend development skills.

---

## 📌 Overview

Schools need a system to manage which students are enrolled in which courses.
This API allows administrators to create students and courses, enroll students, and query relationships between them.

### Key Concepts Demonstrated

* Many-to-many relationships
* DTO pattern
* Entity Framework Core
* SQLite database with migrations
* Validation using Data Annotations
* RESTful API design
* Business logic enforcement (duplicate prevention)

---

## 🧠 Features

### Students

* Create student
* Get all students
* Get student by ID
* Update student
* Delete student

### Courses

* Create course
* Get all courses
* Get course by ID
* Update course
* Delete course

### Enrollments

* Enroll student in course
* Remove enrollment
* Prevent duplicate enrollments
* Auto-set enrollment date

### Relationship Queries

* Get all courses for a student
* Get all students in a course
* Get all enrollments with related data

---

## 🏗️ Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Swagger / OpenAPI
* C#

---

## 📂 Project Structure

```
StudentEnrollment.API/
├── Controllers/
│   ├── CoursesController.cs
│   ├── EnrollmentsController.cs
│   └── StudentsController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── CourseCreateDTO.cs
│   ├── CourseReadDTO.cs
│   ├── CourseUpdateDTO.cs
│   ├── EnrollmentCreateDTO.cs
│   ├── EnrollmentReadDTO.cs
│   ├── StudentCreateDTO.cs
│   ├── StudentReadDTO.cs
│   └── StudentUpdateDTO.cs
├── Helpers/
│   └── MappingHelper.cs
├── Migrations/
│   └── (EF Core migration files)
├── Models/
│   ├── Course.cs
│   ├── Enrollment.cs
│   └── Student.cs
├── Properties/
│   └── launchSettings.json
├── Program.cs
├── appsettings.json
└── StudentEnrollment.API.csproj
```

---

## 🗄️ Data Model

### Student

* Id (long)
* Name (required, max 100)
* Email (required, unique)
* Enrollments (navigation)

### Course

* Id (long)
* Title (required, max 100)
* Credits (1–6)
* Enrollments (navigation)

### Enrollment

* Id (long)
* StudentId (FK)
* CourseId (FK)
* EnrollmentDate (auto-set)
* Navigation properties

---

## 🔗 API Endpoints

### Students

```
GET    /api/students
GET    /api/students/{id}
POST   /api/students
PUT    /api/students/{id}
DELETE /api/students/{id}
```

### Courses

```
GET    /api/courses
GET    /api/courses/{id}
POST   /api/courses
PUT    /api/courses/{id}
DELETE /api/courses/{id}
```

### Enrollments

```
GET    /api/enrollments
GET    /api/enrollments/{id}
POST   /api/enrollments
DELETE /api/enrollments/{id}
```

### Relationship Queries

```
GET /api/students/{id}/courses
GET /api/courses/{id}/students
```

---

## 🚀 Getting Started

### 1. Clone the Repository

```
git clone https://github.com/yourusername/StudentEnrollmentApi.git
cd StudentEnrollmentApi
```

### 2. Restore Dependencies

```
dotnet restore
```

### 3. Apply Migrations

```
dotnet ef database update
```

### 4. Run the Application

```
dotnet run
```

Swagger UI will open automatically:

```
https://localhost:xxxx/swagger
```

---

## ⚙️ Business Rules

* A student cannot enroll in the same course twice
* Email must be unique per student
* Enrollment date is set automatically
* Deletions are permanent
* All endpoints are public (no authentication)

---

## 🧪 Example Enrollment Request

POST `/api/enrollments`

```
{
  "studentId": 1,
  "courseId": 2
}
```

---

## 🎯 What This Project Demonstrates

* Real-world relational modeling
* Clean separation of concerns
* Proper HTTP status codes
* Validation and error handling
* Database constraints
* Navigation property usage
* DTO mapping

---

## 📈 Possible Improvements

* Authentication (JWT)
* Pagination and filtering
* Service layer
* Unit tests
* Logging
* Docker support
* Course capacity limits

---

## 👤 Author

Built as a portfolio project to practice backend API development with ASP.NET Core and Entity Framework.

---

## 📄 License

This project is open-source and available under the MIT License.
