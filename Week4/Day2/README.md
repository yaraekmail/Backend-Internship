
# 🔐 Week 4 — Day 2

## JWT Authentication & Token Issuance

> **JWT • Login • Claims • Bearer Authentication • Protected Endpoints • Token Expiry**

Day 2 focused on extending **ASP.NET Core Identity** with **JWT-based authentication**.

We implemented a login flow that verifies existing users, issues a signed JWT after successful authentication, configures JWT Bearer Authentication, protects an API endpoint using `[Authorize]`, and validates token expiration.

---

## 🛠️ Tech Stack

* **ASP.NET Core 10**
* **ASP.NET Core Identity**
* **Entity Framework Core**
* **SQL Server**
* **JWT Bearer Authentication**
* **System.IdentityModel.Tokens.Jwt**
* **Postman**
* **JWT.io**

---

## 🚀 What We Built

The Day 2 authentication flow:

```text
Existing User
     │
     ▼
POST /api/Auth/login
     │
     ▼
ASP.NET Core Identity
     │
     ▼
CheckPasswordSignInAsync()
     │
     ├── Invalid Credentials ──► 401 Unauthorized
     │
     ▼
Valid Credentials
     │
     ▼
Create JWT
     │
     ├── User ID (sub)
     ├── Email
     ├── Issuer
     ├── Audience
     └── Expiry
     │
     ▼
Return JWT
     │
     ▼
Authorization: Bearer <token>
     │
     ▼
JWT Bearer Validation
     │
     ├── Invalid / Expired ──► 401 Unauthorized
     │
     ▼
[Authorize]
     │
     ▼
Protected Endpoint
```

---

# 🔐 Authentication Flow

The client sends the user's credentials to:

```http
POST /api/Auth/login
```

The API then:

1. Receives the email and password.
2. Finds the user through ASP.NET Core Identity.
3. Verifies the password using `SignInManager.CheckPasswordSignInAsync()`.
4. Returns `401 Unauthorized` for invalid credentials.
5. Creates JWT claims after successful authentication.
6. Signs the token using **HMAC-SHA256**.
7. Adds the configured issuer and audience.
8. Sets the token expiration.
9. Returns the JWT to the client.

The generated token can then be used to authenticate subsequent requests.

---

# 🧩 JWT Structure

A JWT consists of three parts:

```text
Header.Payload.Signature
```

### Header

Describes the token type and signing algorithm.

Our implementation uses:

```text
HS256
```

### Payload

Contains claims related to the authenticated user and token.

The generated token contains claims such as:

```text
sub
email
iss
aud
exp
```

### Signature

The signature allows the API to verify the integrity of the token and detect modification.

> JWT payloads are not encrypted. They can be decoded and read, so sensitive information such as passwords or secret keys must never be stored in JWT claims.

---

# 🏷️ JWT Claims

| Claim       | Purpose                    |
| ----------- | -------------------------- |
| `sub`       | Authenticated user's ID    |
| Email claim | Authenticated user's email |
| `iss`       | Token issuer               |
| `aud`       | Intended audience/API      |
| `exp`       | Token expiration time      |

The generated token was decoded using **JWT.io** to verify these claims.

---

# 🔑 Login Endpoint

### Endpoint

```http
POST /api/Auth/login
```

### Request

The endpoint receives a `LoginRequest` containing:

```text
Email
Password
```

### Successful Login

```text
200 OK
```

The response contains the generated JWT.

### Invalid Login

```text
401 Unauthorized
```

An incorrect password was used to verify that invalid authentication attempts are rejected.

---

# 🎟️ JWT Token Issuance

After successful authentication, the API creates a signed JWT containing the user's identity information.

The token includes:

```text
User ID
Email
Issuer
Audience
Expiry
```

### Signing Algorithm

```text
HMAC-SHA256
```

### Access Token Lifetime

```text
15 minutes
```

---

# 🛡️ JWT Bearer Authentication

JWT Bearer Authentication was configured in `Program.cs`.

The client sends the token through the HTTP Authorization header:

```http
Authorization: Bearer <token>
```

The API validates the incoming token before allowing access to protected endpoints.

The validation includes the configured:

* Issuer
* Audience
* Signing key
* Token lifetime

---

# 🔒 Protected Endpoint

A protected endpoint was implemented using:

```csharp
[Authorize]
```

### Endpoint

```http
GET /api/Auth/protected
```

A valid JWT allows access to the endpoint.

An invalid or expired token results in:

```text
401 Unauthorized
```

---

# ⏱️ Token Expiry

The access token was configured with a short lifetime of:

```text
15 minutes
```

For the expiration test, the JWT validation configuration used:

```csharp
ClockSkew = TimeSpan.Zero
```

This ensured that an expired token would be rejected immediately.

### Expired Token Result

```text
GET /api/Auth/protected
        ↓
Expired JWT
        ↓
Token validation fails
        ↓
401 Unauthorized
```

---

# 🧪 Testing & Evidence

Testing was performed using **Postman**, while JWT claims were inspected using **JWT.io**.

## 1. Successful Login

```http
POST /api/Auth/login
```

**Result:**

```text
200 OK
JWT Token Returned
```

📸 Evidence: Successful Login screenshot in the full documentation.

---

## 2. JWT Claims Verification

The generated token was decoded using JWT.io.

Verified claims:

```text
sub
email
exp
iss
aud
```

🔎 [Open JWT.io](https://www.jwt.io/)

📸 Evidence: JWT.io Claims Verification screenshot in the full documentation.

---

## 3. Invalid Login

```http
POST /api/Auth/login
```

An incorrect password was submitted.

**Result:**

```text
401 Unauthorized
```

📸 Evidence: Invalid Login screenshot in the full documentation.

---

## 4. Expired Token

```http
GET /api/Auth/protected
```

An expired JWT was sent as a Bearer token.

**Result:**

```text
401 Unauthorized
```

📸 Evidence: Expired Token screenshot in the full documentation.

---

# 📁 Project Structure

```text
Day2/
│
├── Controllers/
│   └── AuthController.cs
│
├── Data/
│   └── TrainingManagementDbContext.cs
│
├── Documentation/
│   └── Week4_Day2_Documentation.docx
│
├── Migrations/
│   ├── 20260811192605_InitialCreate.cs
│   ├── 20260811192605_InitialCreate.Designer.cs
│   ├── 20260815163516_AddIdentity.cs
│   ├── 20260815163516_AddIdentity.Designer.cs
│   └── TrainingManagementDbContextModelSnapshot.cs
│
├── Models/
│   ├── Company.cs
│   ├── Course.cs
│   ├── CourseSkill.cs
│   ├── CreateParticipantRequest.cs
│   ├── Instructor.cs
│   ├── IslamicGoal.cs
│   ├── LoginRequest.cs
│   ├── Participant.cs
│   ├── RegisterRequest.cs
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
├── postman/
│   └── Week 4 - Day 2 - JWT Authentication.postman_collection.json
│
├── appsettings.Development.json
├── appsettings.json
├── Day2.csproj
├── Day2.http
├── Program.cs
│
└── Properties/
    └── launchSettings.json
```

> `bin/` and `obj/` are generated build folders and are not included above as part of the main implementation structure.

---

# 📄 Important Files

| File                                                                                                        | Purpose                                                   |
| ----------------------------------------------------------------------------------------------------------- | --------------------------------------------------------- |
| [`Program.cs`](./Program.cs)                                                                                | JWT Bearer Authentication and validation configuration    |
| [`AuthController.cs`](./Controllers/AuthController.cs)                                                      | Registration, Login, JWT creation, and protected endpoint |
| [`LoginRequest.cs`](./Models/LoginRequest.cs)                                                               | Login request model                                       |
| [`RegisterRequest.cs`](./Models/RegisterRequest.cs)                                                         | Registration request model                                |
| [`TrainingManagementDbContext.cs`](./Data/TrainingManagementDbContext.cs)                                   | EF Core + Identity database context                       |
| [`Migrations`](./Migrations/)                                                                               | Database and Identity migration history                   |
| [`Postman Collection`](./postman/Week%204%20-%20Day%202%20-%20JWT%20Authentication.postman_collection.json) | Day 2 API testing collection                              |
| [`Full Documentation`](./Documentation/Week4_Day2_Documentation.docx)                                       | Detailed Day 2 documentation and testing evidence         |

---

# 📚 Resources

### 📄 Project Documentation

[**Week 4 — Day 2 Documentation**](./Documentation/Week4_Day2_Documentation.docx)

Detailed documentation of the implementation, concepts, and testing evidence.

### 🧪 Postman Collection

[**JWT Authentication — Postman Collection**](./postman/Week%204%20-%20Day%202%20-%20JWT%20Authentication.postman_collection.json)

The collection used for Day 2 API testing.

### 🔎 JWT.io

[**JWT.io**](https://www.jwt.io/)

Used to decode and inspect the generated JWT and verify its claims.

### 📖 Microsoft Learn

[**Configure JWT Bearer Authentication in ASP.NET Core**](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0)

Official Microsoft documentation for JWT Bearer Authentication.

---

# 🎓 Learning Outcomes

By the end of Day 2, we were able to:

* Understand the structure of a JWT.
* Work with JWT claims.
* Implement login using ASP.NET Core Identity.
* Verify credentials using `SignInManager.CheckPasswordSignInAsync()`.
* Generate and sign JWT access tokens.
* Configure JWT Bearer Authentication.
* Protect endpoints using `[Authorize]`.
* Validate issuer, audience, signing key, and token lifetime.
* Configure and test token expiration.
* Test authentication scenarios using Postman.
* Inspect JWT claims using JWT.io.

---

# 🚫 Not Implemented

The training material introduced **refresh tokens** as a concept and mentioned them as an optional stretch task.

However, **refresh-token functionality was not implemented in this Day 2 project**.

The implemented authentication flow is:

```text
Login
  ↓
JWT Access Token
  ↓
Bearer Authentication
  ↓
Protected Endpoint
  ↓
Token Expiry
```

---

# ✅ Final Result

Day 2 successfully extended the existing ASP.NET Core Identity authentication system with JWT-based authentication.

The API can now:

* Authenticate existing users through login.
* Issue a signed JWT after successful authentication.
* Return `401 Unauthorized` for invalid credentials.
* Accept JWTs through Bearer Authentication.
* Protect endpoints using `[Authorize]`.
* Validate token issuer, audience, signature, and lifetime.
* Reject expired tokens immediately.
* Verify generated JWT claims through JWT.io.
* Test the complete authentication flow using Postman.

---

## 🏁 Day 2 Summary

> **Implemented a complete JWT authentication flow on top of ASP.NET Core Identity — from login and token issuance to Bearer authentication, protected endpoints, and token-expiry validation.**

---

### 🔗 Quick Access

| Resource                  | Link                                                                                                                                                        |
| ------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 💻 **Main Project**       | [`Day2/`](.)                                                                                                                                                |
| 🔐 **AuthController**     | [`AuthController.cs`](./Controllers/AuthController.cs)                                                                                                      |
| ⚙️ **Program.cs**         | [`Program.cs`](./Program.cs)                                                                                                                                |
| 👤 **LoginRequest**       | [`LoginRequest.cs`](./Models/LoginRequest.cs)                                                                                                               |
| 🗄️ **Database Context**  | [`TrainingManagementDbContext.cs`](./Data/TrainingManagementDbContext.cs)                                                                                   |
| 🧪 **Postman Collection** | [`JWT Authentication`](./postman/Week%204%20-%20Day%202%20-%20JWT%20Authentication.postman_collection.json)                                                 |
| 📄 **Full Documentation** | [`Week4_Day2_Documentation.docx`](./Documentation/Week4_Day2_Documentation.docx)                                                                            |
| 🔎 **JWT.io**             | [JWT.io](https://www.jwt.io/)                                                                                                                               |
| 📖 **Microsoft Learn**    | [JWT Bearer Authentication](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0) |

---

**Week 4 • Day 2**

### 🔐 JWT Authentication & Token Issuance
