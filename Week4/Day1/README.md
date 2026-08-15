# Week 4 - Day 1

## ASP.NET Core Identity & User Registration

## Overview

Day 1 focused on adding **ASP.NET Core Identity** to the existing Training Management backend and implementing user registration.

The Week 3 models, `TrainingManagementDbContext`, and existing SQL Server database were reused, then Identity was added and tested through Postman.

---

## Task

The hands-on task covered:

* Adding ASP.NET Core Identity and EF Core support.
* Extending the existing `DbContext` with `IdentityDbContext<IdentityUser>`.
* Registering Identity in `Program.cs`.
* Creating and applying the `AddIdentity` migration.
* Implementing a user registration endpoint using `UserManager.CreateAsync()`.
* Testing valid and weak-password registration in Postman.

---

## Implementation

### Identity & Database

The existing `TrainingManagementDbContext` was extended to support Identity while keeping the existing Training Management entities.

Identity was connected to the same SQL Server database through EF Core.

[🗄️ TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)

[⚙️ Program.cs](./Program.cs)

### User Registration

A registration request model and authentication controller were implemented.

Endpoint:

```text
POST /api/Auth/register
```

The registration process uses:

```csharp
UserManager<IdentityUser>
```

and:

```csharp
CreateAsync(user, password)
```

[🎮 AuthController.cs](./Controllers/AuthController.cs)

[📝 RegisterRequest.cs](./Models/RegisterRequest.cs)

---

## Database Migration

The `AddIdentity` migration was created and applied successfully.

It added the Identity schema alongside the existing Training Management tables, including:

```text
AspNetUsers
AspNetRoles
AspNetRoleClaims
AspNetUserClaims
AspNetUserLogins
AspNetUserRoles
AspNetUserTokens
```

[🔄 AddIdentity Migration](./Migrations/20260815163516_AddIdentity.cs)

---

## Postman Testing

Registration was tested with two cases:

| Test               | Result              |
| ------------------ | ------------------- |
| Valid registration | `200 OK` ✅          |
| Weak password      | `400 Bad Request` ✅ |

The complete Postman collection is included in the project:

[📦 Week4 Day1 Postman Collection](./Week4Day1.postman_collection.json)

---

## Documentation

Detailed Day 1 documentation and Postman screenshots:

[📄 Day 1 Documentation](./Documentation/Week_4-Day1.docx)

---

## Project Structure

```text
Day1/
├── Program.cs
├── README.md
├── Week4Day1.postman_collection.json
│
├── Controllers/
│   └── AuthController.cs
│
├── Data/
│   └── TrainingManagementDbContext.cs
│
├── Documentation/
│   └── Week_4-Day1.docx
│
├── Migrations/
│   ├── 20260811192605_InitialCreate.cs
│   └── 20260815163516_AddIdentity.cs
│
└── Models/
    ├── RegisterRequest.cs
    └── Training Management models
```

---

## Day 1 Outcome

By the end of Day 1, the existing Training Management backend was extended with **ASP.NET Core Identity**, Identity database tables, and a working user registration endpoint.

Registration was verified in Postman using both a valid password and a deliberately weak password.
