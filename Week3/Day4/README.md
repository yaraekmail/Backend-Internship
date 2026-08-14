

# Week 3 - Day 4 | Training Management API

> A RESTful CRUD API built with ASP.NET Core Web API, Entity Framework Core, and SQL Server, with API testing performed using Postman.

---

## 📌 Overview

This project was developed as part of **Week 3 - Day 4** of the Backend Internship.

The main focus of this day was implementing a complete **CRUD API** for the `Participant` resource using **ASP.NET Core Web API** and **Entity Framework Core**, with **SQL Server** as the database.

The project also includes the training management domain models and the database configuration created during the previous work.

---

## 📚 Table of Contents

- [Overview](#-overview)
- [Topics Covered](#-topics-covered)
- [Technologies Used](#-technologies-used)
- [Project Structure](#-project-structure)
- [Database Layer](#-database-layer)
- [Models](#-models)
- [Participant CRUD API](#-participant-crud-api)
- [HTTP Status Codes](#-http-status-codes)
- [Validation and Error Handling](#-validation-and-error-handling)
- [Postman Testing](#-postman-testing)
- [Hands-On Lab](#-hands-on-lab)
- [Build and Run](#-build-and-run)
- [Documentation](#-documentation)
- [Learning Outcomes](#-learning-outcomes)

---

## 📖 Topics Covered

During Day 4, the following concepts were applied through the implementation:

### RESTful API

The API uses HTTP methods according to their purpose:

| HTTP Method | Purpose |
|---|---|
| `GET` | Retrieve data |
| `POST` | Create a new resource |
| `PUT` | Update an existing resource |
| `DELETE` | Delete a resource |

### CRUD Operations

CRUD represents the four main operations performed on a resource:

```text
Create
Read
Update
Delete
````

These operations were implemented for the `Participant` resource.

### Dependency Injection

ASP.NET Core Dependency Injection was used to provide the `TrainingManagementDbContext` to the `ParticipantsController`.

This allows the controller to access the database without creating the `DbContext` manually.

→ [View Program.cs](./Program.cs)

→ [View ParticipantsController.cs](./Controllers/ParticipantsController.cs)

### Entity Framework Core

Entity Framework Core was used as the ORM to connect the application with SQL Server and perform database operations.

The database context contains `DbSet` properties representing the application's entities.

→ [View TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)

### DTOs

Request DTOs were used for receiving data when creating and updating Participants.

→ [CreateParticipantRequest.cs](./Models/CreateParticipantRequest.cs)

→ [UpdateParticipantRequest.cs](./Models/UpdateParticipantRequest.cs)

### HTTP Status Codes

The API was implemented using appropriate HTTP status codes for successful operations and error cases.

---

## 🛠 Technologies Used

| Technology            | Purpose                 |
| --------------------- | ----------------------- |
| C#                    | Programming language    |
| ASP.NET Core Web API  | Building the REST API   |
| Entity Framework Core | Database access and ORM |
| SQL Server            | Database                |
| Postman               | API testing             |
| .NET 10               | Target framework        |

---

## 📁 Project Structure

```text
Day4/
│
├── Controllers/
│   └── ParticipantsController.cs
│
├── Data/
│   └── TrainingManagementDbContext.cs
│
├── Documentation/
│   └── Week3_Day4_Documentation.docx
│
├── Models/
│   ├── Company.cs
│   ├── Course.cs
│   ├── CourseSkill.cs
│   ├── CreateParticipantRequest.cs
│   ├── Instructor.cs
│   ├── IslamicGoal.cs
│   ├── Participant.cs
│   ├── Semester.cs
│   ├── Skill.cs
│   ├── Task.cs
│   ├── Trainer.cs
│   ├── Training.cs
│   ├── TrainingDay.cs
│   ├── TrainingParticipant.cs
│   ├── TrainingSkill.cs
│   ├── University.cs
│   └── UpdateParticipantRequest.cs
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Day4_TrainingManagementApi.csproj
├── Program.cs
└── README.md
```

---

## 🗄 Database Layer

The project uses **Entity Framework Core** with **SQL Server**.

The main database context is:

[TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)

The `TrainingManagementDbContext` contains `DbSet` properties for the application's entities:

* Participants
* Trainings
* TrainingParticipants
* TrainingDays
* Trainers
* Companies
* Courses
* Instructors
* Universities
* Semesters
* IslamicGoals
* Tasks
* Skills
* CourseSkills
* TrainingSkills

The `DbContext` also contains model configuration for:

* Composite primary keys
* Required properties
* Maximum string lengths
* Unique indexes
* SQL Server column types
* Default values

For example, the Participant email is configured as a unique value.

---

## 🧩 Models

The project contains models representing the training management domain.

### Main Models

| Model               | File                                                      |
| ------------------- | --------------------------------------------------------- |
| Participant         | [Participant.cs](./Models/Participant.cs)                 |
| Training            | [Training.cs](./Models/Training.cs)                       |
| TrainingParticipant | [TrainingParticipant.cs](./Models/TrainingParticipant.cs) |
| TrainingDay         | [TrainingDay.cs](./Models/TrainingDay.cs)                 |
| Trainer             | [Trainer.cs](./Models/Trainer.cs)                         |
| Company             | [Company.cs](./Models/Company.cs)                         |
| Course              | [Course.cs](./Models/Course.cs)                           |
| Instructor          | [Instructor.cs](./Models/Instructor.cs)                   |
| University          | [University.cs](./Models/University.cs)                   |
| Semester            | [Semester.cs](./Models/Semester.cs)                       |
| IslamicGoal         | [IslamicGoal.cs](./Models/IslamicGoal.cs)                 |
| Task                | [Task.cs](./Models/Task.cs)                               |
| Skill               | [Skill.cs](./Models/Skill.cs)                             |
| CourseSkill         | [CourseSkill.cs](./Models/CourseSkill.cs)                 |
| TrainingSkill       | [TrainingSkill.cs](./Models/TrainingSkill.cs)             |

---

# 🔄 Participant CRUD API

The main resource used for the Day 4 CRUD task is:

```text
Participant
```

The endpoints are implemented in:

[ParticipantsController.cs](./Controllers/ParticipantsController.cs)

---

## API Endpoints

| Method   | Endpoint                 | Description             |
| -------- | ------------------------ | ----------------------- |
| `POST`   | `/api/Participants`      | Create a Participant    |
| `GET`    | `/api/Participants`      | Get all Participants    |
| `GET`    | `/api/Participants/{id}` | Get a Participant by ID |
| `PUT`    | `/api/Participants/{id}` | Update a Participant    |
| `DELETE` | `/api/Participants/{id}` | Delete a Participant    |

---

## ➕ Create Participant

### Request

```http
POST /api/Participants
```

The request body uses:

[CreateParticipantRequest.cs](./Models/CreateParticipantRequest.cs)

Example:

```json
{
    "name": "Yara",
    "email": "yara@gmail.com"
}
```

### Successful Response

```text
201 Created
```

The endpoint returns the created Participant and provides a `Location` header for the created resource.

---

## 📋 Get All Participants

### Request

```http
GET /api/Participants
```

This endpoint retrieves all Participants from the database.

### Successful Response

```text
200 OK
```

---

## 🔎 Get Participant by ID

### Request

```http
GET /api/Participants/{id}
```

Example:

```http
GET /api/Participants/1
```

### Possible Responses

| Situation                  | Status          |
| -------------------------- | --------------- |
| Participant exists         | `200 OK`        |
| Participant does not exist | `404 Not Found` |

A deliberate request using a non-existing ID was also tested in Postman.

---

## ✏️ Update Participant

### Request

```http
PUT /api/Participants/{id}
```

The request uses:

[UpdateParticipantRequest.cs](./Models/UpdateParticipantRequest.cs)

Example:

```json
{
    "name": "Updated Name",
    "email": "updated@example.com"
}
```

### Possible Responses

| Situation                  | Status            |
| -------------------------- | ----------------- |
| Update succeeds            | `200 OK`          |
| Participant does not exist | `404 Not Found`   |
| Invalid input              | `400 Bad Request` |

---

## 🗑 Delete Participant

### Request

```http
DELETE /api/Participants/{id}
```

### Possible Responses

| Situation                        | Status           |
| -------------------------------- | ---------------- |
| Participant deleted successfully | `204 No Content` |
| Participant does not exist       | `404 Not Found`  |

A non-existing ID was also tested to verify the error response.

---

# ⚠️ Validation and Error Handling

The API handles different error scenarios instead of treating every request as successful.

The implemented cases include:

* Requesting a Participant that does not exist.
* Updating a Participant that does not exist.
* Deleting a Participant that does not exist.
* Sending invalid input.
* Attempting to create a Participant using an existing email.

The Participant email has a unique index configured in the database context.

Therefore, attempting to insert a duplicate email is rejected by the database.

---

# 🧪 Postman Testing

The CRUD endpoints were tested using **Postman**.

Testing included both successful requests and deliberate error cases.

| Endpoint  | Success Test                | Error Test                    |
| --------- | --------------------------- | ----------------------------- |
| Create    | Create a new Participant    | Duplicate email               |
| Get All   | Retrieve Participants       | —                             |
| Get By ID | Existing ID                 | Non-existing ID               |
| Update    | Update existing Participant | Invalid/non-existing resource |
| Delete    | Delete existing Participant | Non-existing ID               |

The Postman tests were used to verify that the API returned the expected HTTP status codes and responses.

---

# 🧪 Hands-On Lab

The Day 4 hands-on lab required implementing a complete CRUD workflow.

| Requirement                                 | Status |
| ------------------------------------------- | ------ |
| Create endpoint                             | ✅      |
| Return `201 Created`                        | ✅      |
| Return `Location` header                    | ✅      |
| Get-all endpoint                            | ✅      |
| Get-by-ID endpoint                          | ✅      |
| Return `404` for missing ID                 | ✅      |
| Update endpoint                             | ✅      |
| Return `404` for missing resource           | ✅      |
| Return `400` for invalid input              | ✅      |
| Delete endpoint                             | ✅      |
| Return `204 No Content`                     | ✅      |
| Return `404` for missing resource on delete | ✅      |
| Test endpoints in Postman                   | ✅      |
| Test deliberate error cases                 | ✅      |

---

# ▶️ Build and Run

Open a terminal inside the Day 4 project directory.

### Build the project

```powershell
dotnet build
```

The project was successfully built.

### Run the API

```powershell
dotnet run
```

The application runs locally using the configured launch settings.

---

# 📄 Documentation

A detailed documentation file was created for the complete Day 4 work.

📖 **[Open Week 3 Day 4 Documentation](./Documentation/Week3_Day4_Documentation.docx)**

The documentation covers the work completed during the day, including the concepts studied, implementation, CRUD operations, and API testing.

---

# 🎯 Learning Outcomes

By completing Day 4, I practiced:

* Designing RESTful API endpoints
* Implementing CRUD operations
* Working with ASP.NET Core Web API
* Using Entity Framework Core
* Connecting an API to SQL Server
* Using Dependency Injection
* Working with `DbContext` and `DbSet`
* Using DTOs for request data
* Handling missing resources
* Returning appropriate HTTP status codes
* Handling invalid input
* Applying database constraints
* Testing APIs with Postman
* Organizing and documenting a backend project

---

## 🔗 Quick Links

| Resource                  | Link                                                                         |
| ------------------------- | ---------------------------------------------------------------------------- |
| Participants Controller   | [Open ParticipantsController.cs](./Controllers/ParticipantsController.cs)    |
| Database Context          | [Open TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs) |
| Participant Model         | [Open Participant.cs](./Models/Participant.cs)                               |
| Create DTO                | [Open CreateParticipantRequest.cs](./Models/CreateParticipantRequest.cs)     |
| Update DTO                | [Open UpdateParticipantRequest.cs](./Models/UpdateParticipantRequest.cs)     |
| Application Configuration | [Open Program.cs](./Program.cs)                                              |
| Application Settings      | [Open appsettings.json](./appsettings.json)                                  |
| Detailed Documentation    | [Open Documentation](./Documentation/Week3_Day4_Documentation.docx)          |

---

## 📌 Project Information

| Item               | Details                 |
| ------------------ | ----------------------- |
| Week               | 3                       |
| Day                | 4                       |
| Project            | Training Management API |
| Main CRUD Resource | Participant             |
| Framework          | .NET 10                 |
| API                | ASP.NET Core Web API    |
| ORM                | Entity Framework Core   |
| Database           | SQL Server              |
| Testing            | Postman                 |



