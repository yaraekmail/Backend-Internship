# Week 4 — Day 4 | Input Validation with FluentValidation

## ASP.NET Core Web API — FluentValidation & Server-Side Validation

This project extends the **Week 4 authentication and authorization work** by adding **server-side input validation** using **FluentValidation**.

The main focus of Day 4 was validating incoming Create and Update requests before they reach the application logic and database layer.

---

## 🎯 Lab Objectives

* Understand **DataAnnotations vs FluentValidation**.
* Create validators for Create and Update request models.
* Apply validation rules to API requests.
* Register validators using Dependency Injection.
* Integrate FluentValidation with the ASP.NET Core pipeline.
* Return `400 Bad Request` for invalid input.
* Test validation scenarios using Postman.

---

## 🛠 Technologies

| Technology                | Purpose                  |
| ------------------------- | ------------------------ |
| C#                        | Programming language     |
| .NET 10                   | Target framework         |
| ASP.NET Core Web API      | API development          |
| FluentValidation          | Server-side validation   |
| ASP.NET Core Identity     | User and role management |
| JWT Bearer Authentication | Authentication           |
| Entity Framework Core     | ORM and database access  |
| SQL Server                | Database                 |
| Postman                   | API testing              |

---

# 🔎 Validation Flow

The API now validates incoming data before continuing with the normal request processing:

```text
Client Request
      ↓
Authentication
      ↓
Authorization
      ↓
DTO
      ↓
FluentValidation
      ↓
 ┌──────────────┐
 │              │
Valid        Invalid
 │              │
 ↓              ↓
Controller    400 Bad Request
 │
 ↓
Database
```

This prevents invalid data from reaching the controller logic and database operations.

---

# 🧩 Validators

Two validators were created for the Participant request models.

### Create Participant

[CreateParticipantRequest.cs](./Models/CreateParticipantRequest.cs)

is validated by:

[CreateParticipantValidator.cs](./Validators/CreateParticipantValidator.cs)

### Update Participant

[UpdateParticipantRequest.cs](./Models/UpdateParticipantRequest.cs)

is validated by:

[UpdateParticipantValidator.cs](./Validators/UpdateParticipantValidator.cs)

Separating validation into dedicated classes keeps the validation rules outside the controller.

---

# ⚙️ FluentValidation Integration

FluentValidation was registered in:

[Program.cs](./Program.cs)

The application enables automatic validation and discovers validators from the application assembly.

This allows validators such as:

```text
CreateParticipantValidator
UpdateParticipantValidator
```

to be applied automatically to the corresponding request models.

---

# 🧪 API Testing

Validation was tested using the Day 4 Postman collection:

[Week 4 - Day 4 - Input Validation.postman_collection.json](./postman/Week%204%20-%20Day%204%20-%20Input%20Validation.postman_collection.json)

The tests covered valid and invalid Participant requests.

### Expected Behavior

```text
Valid Request
     ↓
Validation Passed
     ↓
Controller
     ↓
Success Response
```

```text
Invalid Request
     ↓
Validation Failed
     ↓
400 Bad Request
```

The API also continues to distinguish other cases such as:

| Scenario                 |             Status |
| ------------------------ | -----------------: |
| Valid request            |        `200 / 201` |
| Invalid input            |  `400 Bad Request` |
| Resource not found       |    `404 Not Found` |
| No valid authentication  | `401 Unauthorized` |
| Insufficient permissions |    `403 Forbidden` |

---

# 🗂️ Project Structure

```text
Day4/
│
├── Controllers/
│   ├── AuthController.cs
│   └── ParticipantsController.cs
│
├── Data/
│   └── TrainingManagementDbContext.cs
│
├── Models/
│   ├── CreateParticipantRequest.cs
│   ├── UpdateParticipantRequest.cs
│   ├── LoginRequest.cs
│   ├── RegisterRequest.cs
│   └── ...
│
├── Validators/
│   ├── CreateParticipantValidator.cs
│   └── UpdateParticipantValidator.cs
│
├── Migrations/
│   ├── InitialCreate
│   ├── AddIdentity
│   └── TrainingManagementDbContextModelSnapshot.cs
│
├── postman/
│   └── Week 4 - Day 4 - Input Validation.postman_collection.json
│
├── Documentation/
│   └── Week_4Day4.docx
│
├── Program.cs
├── Day4.csproj
└── README.md
```

> `bin/` and `obj/` are build/generated folders and are not part of the application source implementation.

---

# 📌 Important Files

### Validation

* [CreateParticipantValidator.cs](./Validators/CreateParticipantValidator.cs)
* [UpdateParticipantValidator.cs](./Validators/UpdateParticipantValidator.cs)

### Request Models

* [CreateParticipantRequest.cs](./Models/CreateParticipantRequest.cs)
* [UpdateParticipantRequest.cs](./Models/UpdateParticipantRequest.cs)

### API

* [ParticipantsController.cs](./Controllers/ParticipantsController.cs)
* [AuthController.cs](./Controllers/AuthController.cs)

### Configuration

* [Program.cs](./Program.cs)
* [Day4.csproj](./Day4.csproj)

### Database

* [TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)
* [Migrations](./Migrations/)

### Testing & Documentation

* [Postman Collection](./postman/Week%204%20-%20Day%204%20-%20Input%20Validation.postman_collection.json)
* [Week 4 Day 4 Documentation](./Documentation/Week_4Day4.docx)

---

# ▶️ Run the Project

From the Day 4 project directory:

```powershell
dotnet restore
dotnet build
dotnet run
```

---

# 🧠 Main Concepts Practiced

* FluentValidation
* Server-side validation
* DTO validation
* Dependency Injection
* ASP.NET Core validation pipeline
* `400 Bad Request`
* Create and Update validation
* Postman API testing
* ASP.NET Core Identity
* JWT Authentication
* Authorization

---

# 🎯 Final Result

By the end of Day 4, the API was extended with a dedicated **input validation layer**.

The overall API flow became:

```text
Identity
   ↓
JWT Authentication
   ↓
Authorization
   ↓
FluentValidation
   ↓
Controller
   ↓
EF Core
   ↓
SQL Server
```

This makes the API more reliable by ensuring that invalid input is rejected before it reaches the application and database layers.

### 🔗 Quick Links

[Program.cs](./Program.cs) ·
[ParticipantsController.cs](./Controllers/ParticipantsController.cs) ·
[CreateParticipantValidator.cs](./Validators/CreateParticipantValidator.cs) ·
[UpdateParticipantValidator.cs](./Validators/UpdateParticipantValidator.cs) ·
[Postman Collection](./postman/Week%204%20-%20Day%204%20-%20Input%20Validation.postman_collection.json) ·
[Documentation](./Documentation/Week_4Day4.docx)
