# Week 4 — Day 5 | API Security & Hardening

## ASP.NET Core Web API — Rate Limiting, CORS, HTTPS & SQL Injection Prevention

This project extends the **Week 4 authentication, authorization, and validation work** by adding additional security configurations to harden the Training Management API.

The main focus of Day 5 was implementing **rate limiting, CORS, HTTPS redirection, HSTS, and reviewing the codebase for raw SQL usage**.

---

## 🎯 Lab Objectives

* Configure rate limiting for API endpoints.
* Apply a stricter rate limit to the login endpoint.
* Configure a named CORS policy.
* Enable HTTPS redirection and HSTS.
* Review the codebase for raw SQL queries.
* Test the implemented security configurations using Postman.

---

## 🛠 Technologies

| Technology                | Purpose                  |
| ------------------------- | ------------------------ |
| C#                        | Programming language     |
| .NET 10                   | Target framework         |
| ASP.NET Core Web API      | API development          |
| ASP.NET Core Identity     | User and role management |
| JWT Bearer Authentication | Authentication           |
| Entity Framework Core     | ORM and database access  |
| SQL Server                | Database                 |
| Postman                   | API testing              |

---

# 🛡️ API Security Flow

The API now includes additional security layers around the existing authentication and authorization system:

```text
Client Request
      ↓
HTTPS / HSTS
      ↓
CORS
      ↓
Rate Limiting
      ↓
JWT Authentication
      ↓
Authorization
      ↓
Controller
      ↓
EF Core
      ↓
SQL Server
```

---

# 🚦 Rate Limiting

ASP.NET Core's built-in rate limiting was configured in:

[Program.cs](./Program.cs)

Two policies were created:

### LoginPolicy

The login endpoint allows:

```text
5 requests per minute
```

and returns:

```text
429 Too Many Requests
```

when the limit is exceeded.

The policy is applied in:

[AuthController.cs](./Controllers/AuthController.cs)

### GeneralPolicy

General API requests allow:

```text
100 requests per minute
```

The policy is applied to the Participant `GetById` endpoint in:

[ParticipantsController.cs](./Controllers/ParticipantsController.cs)

---

# 🌐 CORS

A named CORS policy called:

```text
AllowFrontend
```

was configured in:

[Program.cs](./Program.cs)

The configured frontend origin is:

```text
https://myapp.com
```

The policy allows the frontend to send requests using different HTTP methods and headers.

---

# 🔒 HTTPS & HSTS

HTTPS redirection and HSTS were enabled in:

[Program.cs](./Program.cs)

The local HTTP and HTTPS application URLs are configured in:

[launchSettings.json](./Properties/launchSettings.json)

This ensures that HTTP requests are redirected to HTTPS and that browsers are instructed to prefer HTTPS.

---

# 🛡️ SQL Injection Prevention

The C# codebase was reviewed for raw SQL usage using:

```text
FromSql
ExecuteSql
SqlQuery
```

No matching raw SQL usage was found.

The API continues to use **Entity Framework Core and LINQ** for database operations.

For example:

```csharp
var participant = await _context.Participants
    .FirstOrDefaultAsync(p => p.Id == id);
```

This keeps database access within the EF Core abstraction instead of manually constructing SQL queries.

---

# 🧪 API Testing

The security configurations were tested using the Day 5 Postman collection:

[Week 4 - Day 5 - API Hardening.postman_collection.json](./postman/Week%204%20-%20Day5%20-%20API%20Hardening.postman_collection.json)

The tests covered:

| Security Feature      | Result                                            |
| --------------------- | ------------------------------------------------- |
| Login rate limiting   | `429 Too Many Requests` after exceeding the limit |
| General rate limiting | Policy configured                                 |
| CORS                  | Allowed and disallowed origins tested             |
| HTTPS                 | Configured                                        |
| HSTS                  | Configured                                        |
| Raw SQL review        | No matching raw SQL usage found                   |

---

# 🗂️ Project Structure

```text
Day5/
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
│   └── Week 4 - Day5 - API Hardening.postman_collection.json
│
├── Documentation/
│   └── Week4Day_5.docx
│
├── Properties/
│   └── launchSettings.json
│
├── Program.cs
├── Day5.csproj
└── README.md
```

> `bin/` and `obj/` are build/generated folders and are not part of the application source implementation.

---

# 📌 Important Files

### Security Configuration

* [Program.cs](./Program.cs)
* [AuthController.cs](./Controllers/AuthController.cs)
* [ParticipantsController.cs](./Controllers/ParticipantsController.cs)

### Configuration

* [launchSettings.json](./Properties/launchSettings.json)
* [Day5.csproj](./Day5.csproj)

### Database

* [TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)
* [Migrations](./Migrations/)

### Testing & Documentation

* [Postman Collection](./postman/Week%204%20-%20Day5%20-%20API%20Hardening.postman_collection.json)
* [Week 4 Day 5 Documentation](./Documentation/Week4Day_5.docx)

---

# ▶️ Run the Project

From the Day 5 project directory:

```powershell
dotnet restore
dotnet build
dotnet run
```

---

# 🧠 Main Concepts Practiced

* Rate Limiting
* Fixed Window Rate Limiting
* CORS
* HTTPS Redirection
* HSTS
* SQL Injection Prevention
* Entity Framework Core
* ASP.NET Core Identity
* JWT Authentication
* Postman API Testing

---

# 🎯 Final Result

By the end of Day 5, the Training Management API was extended with additional **security hardening measures**.

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
Security Hardening
   ├── Rate Limiting
   ├── CORS
   ├── HTTPS
   └── HSTS
   ↓
Controller
   ↓
EF Core
   ↓
SQL Server
```

The API therefore includes authentication, authorization, input validation, and additional security protections against common API-level risks.

### 🔗 Quick Links

[Program.cs](./Program.cs) ·
[AuthController.cs](./Controllers/AuthController.cs) ·
[ParticipantsController.cs](./Controllers/ParticipantsController.cs) ·
[launchSettings.json](./Properties/launchSettings.json) ·
[Postman Collection](./postman/Week%204%20-%20Day5%20-%20API%20Hardening.postman_collection.json) ·
[Documentation](./Documentation/Week4Day_5.docx)
