# BINX TECH • BACKEND DEVELOPMENT INTERNSHIP PROGRAM (.NET)

## Week 4 — Authentication, Identity & Input Validation

> **Phase 2 • Core Technical Training**
> **Duration:** 40 Hours • 5 Training Days
> **Focus:** Authentication, Authorization, Input Validation & API Security

---

## 📌 Week 4 Overview

Week 4 builds the security layer on top of the REST API developed in **Week 3**.

The API is moved from an open CRUD-based system to a secured API with:

* ASP.NET Core Identity
* JWT Authentication
* Role-Based Authorization
* FluentValidation
* Rate Limiting
* CORS
* Security Headers
* SQL Injection Protection through EF Core parameterized queries

The main goal is to ensure that:

> **Users are authenticated, access is authorized, incoming data is validated, and the API is hardened against common security risks.**

---

## 🗂️ Navigation

* [Week 4 Objectives](#-week-4-objectives)
* [Day 1 — ASP.NET Core Identity](#-day-1--aspnet-core-identity--user-registration)
* [Day 2 — JWT Authentication](#-day-2--jwt-authentication--token-issuance)
* [Day 3 — Authorization & RBAC](#-day-3--authorization--role-based-access-control)
* [Day 4 — FluentValidation](#-day-4--input-validation-with-fluentvalidation)
* [Day 5 — API Security Hardening](#-day-5--api-security-hardening)
* [Security Flow](#-week-4-security-flow)
* [Deliverables](#-week-4-deliverables)
* [Evaluation Criteria](#-evaluation-criteria)
* [Technical Stack](#-technical-stack)
* [Key Takeaways](#-key-takeaways)

---

# 🎯 Week 4 Objectives

By the end of Week 4, the API should be able to:

* Manage users securely using **ASP.NET Core Identity**.
* Register users with securely hashed passwords.
* Authenticate users through **JWT-based login**.
* Validate JWT bearer tokens before protected endpoints execute.
* Protect API routes using `[Authorize]`.
* Restrict sensitive operations using **roles**.
* Apply claims-based and policy-based authorization where required.
* Validate incoming requests using **FluentValidation**.
* Return structured validation errors.
* Apply **rate limiting** to reduce abuse and brute-force attempts.
* Configure **CORS** for trusted frontend origins.
* Apply baseline security headers and HTTPS protection.
* Understand how EF Core parameterized queries protect against SQL injection.

---

# 🔐 Day 1 — ASP.NET Core Identity & User Registration

### Focus

**User Management + Secure Password Storage**

ASP.NET Core Identity provides the foundation for managing users, passwords, roles, and authentication-related data.

Instead of creating a custom users table and implementing password hashing manually, Identity provides the required functionality on top of Entity Framework Core.

### What We Covered

#### ASP.NET Core Identity

Identity provides:

* User management
* Password hashing
* Role management
* Account-related functionality
* Integration with Entity Framework Core

The application's `DbContext` is extended through:

`IdentityDbContext<IdentityUser>`

This adds the Identity schema alongside the application's existing database entities.

### User Registration

Registration uses:

`UserManager<TUser>.CreateAsync()`

Identity handles password hashing and persistence.

The registration flow is therefore:

```text
Client
  ↓
Registration Request
  ↓
Request Validation
  ↓
UserManager.CreateAsync()
  ↓
Password Hashing
  ↓
User Stored in Database
```

### Password Security

Identity handles password hashing instead of storing plaintext passwords.

The default password hashing approach uses **PBKDF2** with a unique salt per password.

> Password hashing should not be implemented manually when ASP.NET Core Identity already provides the required mechanism.

### Hands-On

* Add Identity packages.
* Extend the existing `DbContext`.
* Create and apply the Identity migration.
* Register Identity services.
* Implement user registration.
* Test successful registration.
* Test registration with an intentionally weak password.

**Tools:** ASP.NET Core Identity • EF Core • Postman

---

# 🔑 Day 2 — JWT Authentication & Token Issuance

### Focus

**Login + JWT Creation + Token Validation**

After registration, users need a secure way to authenticate and access protected resources.

JWT is used as the authentication mechanism.

### JWT Structure

A JWT consists of three parts:

```text
Header.Payload.Signature
```

### Header

Contains information about the token, including the signing algorithm.

### Payload

Contains claims representing information about the authenticated user.

Examples:

* User ID
* Email
* Roles

The payload is **encoded, not encrypted**, so sensitive information should not be placed inside JWT claims.

### Signature

The signature allows the server to verify that the token was generated with the expected signing key and has not been modified.

---

## Login Flow

```text
Client
  ↓
Login Request
  ↓
Identity verifies credentials
  ↓
Credentials valid?
  ├── No  → 401 Unauthorized
  │
  └── Yes
       ↓
   Create Claims
       ↓
   Sign JWT
       ↓
   Return Token
```

After login, the client sends the JWT with subsequent protected requests instead of repeatedly sending the user's credentials.

---

## JWT Bearer Authentication

JWT bearer authentication is configured so ASP.NET Core can validate incoming tokens.

The validation process checks things such as:

* Signature
* Issuer
* Audience
* Expiration

For an endpoint protected with `[Authorize]`, the authentication process happens **before the endpoint's own code executes**.

```text
HTTP Request
     ↓
Authentication Middleware
     ↓
JWT Validation
     ↓
Valid Token?
   ↙       ↘
 No         Yes
 ↓           ↓
401      Endpoint
             ↓
        Business Logic
```

### Token Expiration

Access tokens should have a limited lifetime.

A short-lived token reduces the period during which a stolen token can be abused.

Refresh tokens were introduced conceptually as a mechanism for obtaining a new access token without forcing the user to log in again.

### Secret Management

The JWT signing key is sensitive information.

It should not be committed to source control in plaintext.

---

### Hands-On

* Implement login.
* Verify credentials using Identity.
* Return `401 Unauthorized` for invalid credentials.
* Generate a signed JWT after successful authentication.
* Add user claims.
* Configure JWT bearer authentication.
* Configure token expiration.
* Test the token through Postman.
* Verify that an expired token is rejected.

**Tools:** ASP.NET Core Identity • JWT • Postman

---

# 🛡️ Day 3 — Authorization & Role-Based Access Control

### Focus

**Who are you? → Authentication**
**What are you allowed to do? → Authorization**

Authentication confirms the user's identity.

Authorization determines whether that authenticated user has permission to perform a specific action.

---

## `[Authorize]`

Applying `[Authorize]` to a controller or endpoint requires an authenticated user.

Requests without:

* A token
* A valid token
* A non-expired token

are rejected with:

`401 Unauthorized`

The endpoint code does not execute.

---

## Role-Based Access Control

Identity roles are used to control access to sensitive operations.

The Week 4 baseline uses at least two roles:

```text
User
Admin
```

For example:

```text
[Authorize(Roles = "Admin")]
```

This means that the endpoint requires the user to be authenticated **and** have the `Admin` role.

### 401 vs 403

| Status             | Meaning                                            |
| ------------------ | -------------------------------------------------- |
| `401 Unauthorized` | User is not successfully authenticated             |
| `403 Forbidden`    | User is authenticated but does not have permission |

Example:

```text
No Token
   ↓
401 Unauthorized

Valid User Token
   ↓
Admin-only Endpoint
   ↓
403 Forbidden

Valid Admin Token
   ↓
Endpoint
   ↓
Success
```

---

## Claims-Based & Policy-Based Authorization

Roles are one form of authorization.

Claims and policies allow more specific authorization rules.

Instead of checking only:

```text
Is the user an Admin?
```

a policy can represent a more specific requirement such as:

```text
CanManageOrders
```

This keeps authorization rules centralized and easier to maintain.

---

## Protected Route Testing

Protected endpoints are tested through Postman using:

```text
Authorization
    ↓
Bearer Token
    ↓
JWT
```

The login token can be captured and reused through Postman environment variables.

### Hands-On

* Protect the existing CRUD endpoints.
* Test requests without a token.
* Create `User` and `Admin` roles.
* Assign users to roles.
* Restrict the Delete operation to Admin.
* Verify `401` for unauthenticated requests.
* Verify `403` for authenticated users without permission.
* Add one authorization policy beyond a simple role check.
* Reuse JWT tokens through Postman.

**Tools:** ASP.NET Core Identity • Postman

---

# ✅ Day 4 — Input Validation with FluentValidation

### Focus

**Reject invalid input before it reaches business logic or the database.**

FluentValidation provides a dedicated way to express validation rules outside the data model.

---

## DataAnnotations vs FluentValidation

### DataAnnotations

Useful for simple validation rules such as:

* Required fields
* Maximum length
* Basic property constraints

### FluentValidation

Provides a cleaner approach when validation becomes more complex.

Examples:

```text
CustomerId > 0
Discount between 0 and 100
EndDate > StartDate
Order must contain at least one item
```

The validation logic is kept in dedicated validator classes instead of being scattered throughout the application.

---

## Validator Structure

A validator inherits from:

`AbstractValidator<T>`

and defines rules using `RuleFor`.

Example structure:

```text
Request Model
     ↓
Validator
     ↓
Validation Rules
     ↓
Valid?
 ┌───┴───┐
 No      Yes
 ↓        ↓
400      Endpoint
```

---

## Pipeline Integration

FluentValidation is integrated into the ASP.NET Core pipeline so validation happens before the endpoint's business logic executes.

Invalid requests return:

`400 Bad Request`

with structured validation information.

This prevents controllers and services from being filled with repeated input checks.

---

## Validation Response

Validation errors should clearly identify:

* Which field failed.
* Why it failed.

The expected response format is structured using ASP.NET Core's `ValidationProblemDetails`.

---

## Hands-On

* Install FluentValidation and its ASP.NET Core integration.
* Create a validator for the Create request.
* Add at least three real business rules.
* Create a validator for the Update request.
* Register the validators.
* Test each rule separately through Postman.
* Verify structured `400` responses.

**Tools:** FluentValidation • Postman

---

# 🧱 Day 5 — API Security Hardening

### Focus

**Rate Limiting + CORS + Security Headers + SQL Injection Protection**

The final day hardens the API against common production security risks.

---

## Rate Limiting

Rate limiting controls how many requests a client can make within a defined period.

It helps reduce:

* Brute-force login attempts
* Excessive requests
* Simple denial-of-service patterns

Sensitive endpoints such as login should have stricter limits than general endpoints.

The implementation can use:

* Built-in .NET rate limiting
* AspNetCoreRateLimit

---

## CORS

CORS controls which browser-based origins are allowed to call the API.

A production API should not blindly allow every origin.

Instead, a named policy should allow only trusted frontend origins.

```text
Frontend Origin
       ↓
      CORS
       ↓
Allowed?
  ↙       ↘
No         Yes
↓           ↓
Reject      API
```

---

## Security Headers

The API applies baseline HTTP security protections including:

* HTTPS redirection
* HSTS
* Content Security Policy

These protections help reduce common web security risks and enforce secure communication.

---

# 💉 SQL Injection Protection

Entity Framework Core parameterizes values in LINQ queries automatically.

This means user input is not directly concatenated into SQL commands when normal LINQ querying is used.

The main risk appears when raw SQL is constructed unsafely.

### Safe Approach

Use parameterized queries such as:

```text
FromSqlInterpolated
```

or explicit SQL parameters.

### Unsafe Approach

Avoid building raw SQL by directly inserting user input into the SQL string.

```text
User Input
    ↓
String Concatenation
    ↓
Raw SQL
    ↓
Potential SQL Injection
```

---

## Hands-On

* Configure rate limiting.
* Apply stricter limits to login.
* Configure a named CORS policy.
* Allow only the intended frontend origin.
* Test a disallowed origin.
* Enable HTTPS redirection.
* Enable HSTS.
* Review raw SQL usage.
* Confirm that user input is not inserted into SQL through unsafe string interpolation.

**Tools:** .NET Rate Limiting / AspNetCoreRateLimit • ASP.NET Core • Notion

---

# 🔄 Week 4 Security Flow

The security architecture developed throughout the week can be viewed as a layered pipeline:

```text
                    HTTP Request
                         │
                         ▼
                  ┌─────────────┐
                  │ Rate Limit  │
                  └──────┬──────┘
                         │
                         ▼
                  ┌─────────────┐
                  │    CORS     │
                  └──────┬──────┘
                         │
                         ▼
                  ┌─────────────┐
                  │Authentication│
                  │    JWT      │
                  └──────┬──────┘
                         │
                  Valid Token?
                    /         \
                  No           Yes
                  │             │
                 401            ▼
                         ┌─────────────┐
                         │Authorization│
                         │   Roles     │
                         └──────┬──────┘
                                │
                         Has Permission?
                           /          \
                         No            Yes
                         │              │
                        403             ▼
                                ┌─────────────┐
                                │ Validation  │
                                │FluentValid. │
                                └──────┬──────┘
                                       │
                                Valid Request?
                                  /         \
                                No           Yes
                                │             │
                               400            ▼
                                      Endpoint / Service
                                             │
                                             ▼
                                          EF Core
                                             │
                                             ▼
                                          Database
```

---

# 📦 Week 4 Deliverables

By the end of the week, the API should contain:

* [ ] Working user registration using ASP.NET Core Identity.
* [ ] Secure password hashing through Identity.
* [ ] Working login flow.
* [ ] JWT token issuance.
* [ ] JWT bearer authentication.
* [ ] Token expiration.
* [ ] Protected API routes.
* [ ] At least two roles: `User` and `Admin`.
* [ ] Role-based access control.
* [ ] At least one authorization policy.
* [ ] FluentValidation for Create requests.
* [ ] FluentValidation for Update requests.
* [ ] Structured validation error responses.
* [ ] Rate limiting.
* [ ] Named CORS policy.
* [ ] HTTPS redirection.
* [ ] HSTS.
* [ ] Security header configuration.
* [ ] Review of raw SQL usage for SQL injection risks.
* [ ] Week 4 summary prepared for mentor review.

---

# 📊 Evaluation Criteria

| Area                      | Developing                    | Proficient                               | Excellent                                                 |
| ------------------------- | ----------------------------- | ---------------------------------------- | --------------------------------------------------------- |
| Authentication & Security | Basic/insecure authentication | JWT, validation & CORS                   | RBAC, rate limiting, secure headers & environment secrets |
| C# & .NET                 | Basic Identity/JWT usage      | Correct Identity, JWT & FluentValidation | Idiomatic, policy-based authorization                     |
| API Design                | Inconsistent errors           | Correct `400/401/403` responses          | Consistent structured errors                              |
| Code Quality              | Basic structure               | Clean & consistent                       | Production-grade & modular                                |
| Problem Solving           | Requires significant guidance | Resolves most issues                     | Debugs independently and explains root causes             |
| Attendance                | 3–6 absences                  | 1–2 absences                             | Perfect attendance & proactive participation              |

---

# 🧰 Technical Stack

| Category       | Technology                      |
| -------------- | ------------------------------- |
| Framework      | ASP.NET Core                    |
| Identity       | ASP.NET Core Identity           |
| Authentication | JWT Bearer Authentication       |
| Authorization  | `[Authorize]`, Roles, Policies  |
| ORM            | Entity Framework Core           |
| Validation     | FluentValidation                |
| Security       | Rate Limiting, CORS, HTTPS/HSTS |
| Database       | SQL Server                      |
| API Testing    | Postman                         |
| Documentation  | Notion                          |

---

# 🧠 Key Takeaways

### Authentication

**Authentication answers:**

> Who is the user?

Implemented through:

`ASP.NET Core Identity + JWT`

### Authorization

**Authorization answers:**

> What is the user allowed to do?

Implemented through:

`[Authorize] + Roles + Policies`

### Validation

**Validation answers:**

> Is the incoming request acceptable?

Implemented through:

`FluentValidation`

### Hardening

**Security hardening answers:**

> How do we reduce abuse and common attack surfaces?

Implemented through:

`Rate Limiting + CORS + Security Headers + HTTPS`

### Database Security

**EF Core answers:**

> How do we prevent normal user input from becoming executable SQL?

Through parameterized queries in normal LINQ operations.

---

# 🔗 Week 4 at a Glance

```text
DAY 1
ASP.NET Core Identity
        │
        ▼
User Registration
        │
        ▼
Password Hashing
        │
        ▼
DAY 2
JWT Authentication
        │
        ▼
Login + Token Issuance
        │
        ▼
DAY 3
Authorization
        │
        ▼
Roles + Policies
        │
        ▼
Protected Routes
        │
        ▼
DAY 4
FluentValidation
        │
        ▼
Request Validation
        │
        ▼
Structured 400 Errors
        │
        ▼
DAY 5
Security Hardening
        │
        ├── Rate Limiting
        ├── CORS
        ├── HTTPS / HSTS
        ├── Security Headers
        └── SQL Injection Protection
```

---

## 🚀 Week 4 Outcome

At the end of Week 4, the Week 3 CRUD API is no longer simply an API that **works**.

It becomes an API that:

**Authenticates users → Authorizes access → Validates requests → Protects resources → Handles abuse → Follows security baselines.**

This establishes the security foundation required before moving into **Week 5: Testing, Reliability & Capstone Development**.
