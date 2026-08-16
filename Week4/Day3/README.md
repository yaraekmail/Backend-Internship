# Week 4 — Day 3 | Authorization & RBAC

## ASP.NET Core Web API — JWT Authentication, Roles & Policies

This project extends the Week 4 authentication work by adding **route protection, role-based authorization, policy-based authorization, password reset for testing, and automated JWT reuse in Postman**.

The implementation uses **ASP.NET Core Identity + JWT Bearer Authentication** to authenticate users and control access to protected API endpoints.

---

## 🎯 Lab Objectives

* Protect existing CRUD endpoints using `[Authorize]`.
* Create `User` and `Admin` roles.
* Assign roles to different test users using ASP.NET Core Identity.
* Restrict the `Delete` operation to `Admin` users.
* Verify the difference between `401 Unauthorized` and `403 Forbidden`.
* Add a named authorization policy with more than a simple role check.
* Configure Postman to automatically save and reuse the JWT.
* Verify all authorization scenarios through Postman.

---

## 🛠 Technologies

| Technology                | Purpose                  |
| ------------------------- | ------------------------ |
| C#                        | Programming language     |
| .NET 10                   | Target framework         |
| ASP.NET Core Web API      | API development          |
| ASP.NET Core Identity     | User and role management |
| JWT Bearer Authentication | Authentication           |
| Entity Framework Core     | ORM / database access    |
| SQL Server                | Database                 |
| Postman                   | API testing              |
| Git / GitHub              | Version control          |

---

# 🔐 Authentication & Authorization

The project uses two related security concepts:

### Authentication

Authentication answers:

> **Who is the user?**

The user sends an email and password to:

```text
POST /api/Auth/login
```

After successful authentication, the API generates a JWT.

The JWT contains claims such as the user's identity information and role.

### Authorization

Authorization answers:

> **What is the authenticated user allowed to do?**

The API uses:

* `[Authorize]`
* Role-based authorization
* Named authorization policies

This allows different users to access different operations.

---

# 🔑 JWT Authentication Flow

```text
User
 │
 │ Email + Password
 ▼
POST /api/Auth/login
 │
 ▼
ASP.NET Core Identity
 │
 │ Verify credentials
 ▼
User + Roles
 │
 ▼
JWT Generation
 │
 ▼
JWT Token
 │
 ▼
Postman
 │
 │ Bearer Token
 ▼
Protected Endpoint
 │
 ▼
Authentication
 │
 ▼
Authorization
 │
 ▼
Endpoint
```

The JWT is then sent with protected requests as a Bearer token.

---

# 🛡️ Protecting the Participants Controller

The existing CRUD controller was protected using:

```csharp
[Authorize]
```

This was applied to:

[ParticipantsController.cs](./Controllers/ParticipantsController.cs)

The controller contains the Participant CRUD operations:

```text
POST   /api/Participants
GET    /api/Participants
GET    /api/Participants/{id}
PUT    /api/Participants/{id}
DELETE /api/Participants/{id}
```

After adding `[Authorize]`, requests without a valid authentication token are rejected before the protected endpoint executes.

---

# 👥 Roles

Two roles were created:

```text
User
Admin
```

The roles were managed using ASP.NET Core Identity.

The role-management logic is implemented in:

[AuthController.cs](./Controllers/AuthController.cs)

The test users used during the lab were:

| User                 | Role    |
| -------------------- | ------- |
| `testuser@gmail.com` | `User`  |
| `yara@gmail.com`     | `Admin` |

Role assignment was performed through `UserManager`.

---

# 👑 Admin-Only Authorization

The `Delete` operation was restricted to the `Admin` role.

The authorization rule is implemented in:

[ParticipantsController.cs](./Controllers/ParticipantsController.cs)

The important behavior is:

```text
Authenticated User
       │
       ├── Admin ───────► Delete allowed
       │
       └── User ─────────► 403 Forbidden
```

This demonstrates the difference between authentication and authorization.

A valid JWT is not enough when the endpoint requires a specific role.

---

# 📋 Named Authorization Policy

In addition to a simple role check, a named authorization policy was implemented.

The policy configuration is located in:

[Program.cs](./Program.cs)

The policy is more specific than simply checking whether a user is authenticated.

It evaluates the claims/role of the authenticated user before allowing access to the protected endpoint.

The policy is then applied to the relevant endpoint using:

```csharp
[Authorize(Policy = "...")]
```

This demonstrates **policy-based authorization** in addition to role-based authorization.

---

# 🔄 Password Reset for Testing

During the lab, we also needed to reset the password of the Yara test account so that we could authenticate successfully as the Admin user.

The password-reset logic was implemented using ASP.NET Core Identity through:

* `UserManager`
* `GeneratePasswordResetTokenAsync`
* `ResetPasswordAsync`

The implementation is located in:

[AuthController.cs](./Controllers/AuthController.cs)

This was used only to prepare the test account for the authentication and authorization tests.

---

# 🧪 Authorization Test Results

The API was tested through Postman using different authentication states.

| Scenario                             |  Expected Result | Status |
| ------------------------------------ | ---------------: | -----: |
| Login with valid credentials         |     JWT returned |  ✅ 200 |
| Protected endpoint without token     | Request rejected |  ✅ 401 |
| Create `User` and `Admin` roles      |    Roles created |  ✅ 200 |
| Assign roles to test users           |   Roles assigned |  ✅ 200 |
| Reset Yara's password                | Password changed |  ✅ 200 |
| Login as Yara/Admin                  |     JWT returned |  ✅ 200 |
| Admin-only request using Yara        |   Access allowed |      ✅ |
| Login as TestUser                    |     JWT returned |  ✅ 200 |
| Protected `GetAll` without token     | Request rejected |  ✅ 401 |
| Delete using User token              |    Access denied |  ✅ 403 |
| Policy-protected request using Admin |   Access allowed |  ✅ 200 |

### `401 Unauthorized`

Occurs when the request is not successfully authenticated.

```text
No valid token
      ↓
Authentication fails
      ↓
401 Unauthorized
```

### `403 Forbidden`

Occurs when the user is authenticated but does not have the required permission.

```text
Valid JWT
   ↓
User authenticated
   ↓
Role = User
   ↓
Admin required
   ↓
403 Forbidden
```

---

# 📮 Postman

The complete Postman collection used during the lab was exported and included in the project:

[Week 4 - Day 3 - Authorization & RBAC.postman_collection.json](./postman/Week%204%20-%20Day%203%20-%20Authorization%20%26%20RBAC.postman_collection.json)

The collection contains the authentication and authorization tests used during the implementation.

Postman was also configured to save the JWT into an environment variable:

```text
token
```

The saved token can then be reused in protected requests as:

```text
{{token}}
```

This avoids manually copying the JWT for every request.

---

# 🗂️ Project Structure

```text
Day3/
│
├── Controllers/
│   ├── AuthController.cs
│   └── ParticipantsController.cs
│
├── Data/
│   └── TrainingManagementDbContext.cs
│
├── Migrations/
│   ├── InitialCreate
│   ├── AddIdentity
│   └── TrainingManagementDbContextModelSnapshot.cs
│
├── Models/
│   ├── LoginRequest.cs
│   ├── RegisterRequest.cs
│   ├── CreateParticipantRequest.cs
│   ├── UpdateParticipantRequest.cs
│   ├── Participant.cs
│   └── ...
│
├── Documentation/
│   └── Week_4-Day3.docx
│
├── postman/
│   └── Week 4 - Day 3 - Authorization & RBAC.postman_collection.json
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── appsettings.Development.json
├── Day3.csproj
├── Day3.http
└── Program.cs
```

> `bin/` and `obj/` are build/generated folders and are not part of the application source implementation.

---

# 🔗 Important Code Files

### Authentication & Identity

* [AuthController.cs](./Controllers/AuthController.cs)

  * Login
  * JWT generation
  * Identity user operations
  * Role creation/assignment
  * Password reset/testing flow

* [LoginRequest.cs](./Models/LoginRequest.cs)

  * Login request model

* [RegisterRequest.cs](./Models/RegisterRequest.cs)

  * Registration request model

### Authorization

* [ParticipantsController.cs](./Controllers/ParticipantsController.cs)

  * `[Authorize]`
  * Protected CRUD endpoints
  * Admin-only Delete
  * Policy-protected endpoint

* [Program.cs](./Program.cs)

  * JWT Bearer authentication
  * Identity configuration
  * Authorization configuration
  * Named policy configuration
  * Application pipeline

### Database & Identity

* [TrainingManagementDbContext.cs](./Data/TrainingManagementDbContext.cs)

  * EF Core DbContext
  * Identity database integration

* [Migrations](./Migrations/)

  * Database migration history
  * Identity tables and application tables

### API Testing

* [Postman Collection](./postman/Week%204%20-%20Day%203%20-%20Authorization%20%26%20RBAC.postman_collection.json)

---

# ▶️ Run the Project

From the project directory:

```powershell
dotnet restore
dotnet build
dotnet run
```

The application uses the configuration defined in:

[appsettings.json](./appsettings.json)

and:

[appsettings.Development.json](./appsettings.Development.json)

---

# 🗄️ Database

The project uses:

```text
Entity Framework Core
        +
SQL Server
        +
ASP.NET Core Identity
```

Identity adds the tables required for users and roles, including:

```text
AspNetUsers
AspNetRoles
```

The database history is represented by the migrations in:

[Migrations](./Migrations/)

---

# 📚 Main Concepts Practiced

### Authentication

* ASP.NET Core Identity
* Login
* Password verification
* JWT generation
* JWT claims
* Bearer authentication

### Authorization

* `[Authorize]`
* Role-based authorization
* `User` vs `Admin`
* Admin-only endpoints
* Named authorization policies
* Claims-based authorization
* `401 Unauthorized`
* `403 Forbidden`

### API Testing

* Postman
* Bearer tokens
* Environment variables
* Automatic JWT storage
* Reusing `{{token}}`

---

# 🎯 Final Result

By the end of Day 3, the API was no longer simply checking whether a request contained a token.

It could distinguish between:

```text
Anonymous User
      ↓
     401
```

```text
Authenticated User
      ↓
Insufficient Role
      ↓
     403
```

```text
Authenticated Admin
      ↓
Required Authorization
      ↓
    200 OK
```

The project therefore demonstrates the complete flow from **Identity → Login → JWT → Authentication → Roles → Policies → Protected API endpoints → Postman testing**.

---
