# Week 4 — Backend Security & API Hardening

## Overview

Week 4 focused on strengthening the **Training Management API** by adding authentication, authorization, input validation, and API security configurations.

The week followed a continuous security workflow: implementing JWT authentication, protecting endpoints with roles and policies, validating incoming requests, and hardening the API with rate limiting, CORS, HTTPS, HSTS, and SQL injection prevention review.

---

## Week 4 Structure

| Day                       | Topic                                  | Summary                                                                                                                          |
| ------------------------- | -------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| [Day 1](./Day1/README.md) | JWT Authentication & Token Issuance    | Implemented JWT-based authentication, login, token generation, claims, and JWT Bearer configuration.                             |
| [Day 2](./Day2/README.md) | JWT Authentication Configuration       | Configured JWT authentication and integrated token validation into the ASP.NET Core request pipeline.                            |
| [Day 3](./Day3/README.md) | Authorization & RBAC                   | Added `[Authorize]`, User/Admin roles, role-based authorization, authorization policies, and protected endpoints.                |
| [Day 4](./Day4/README.md) | Input Validation with FluentValidation | Added server-side validation for Create and Update requests using FluentValidation and tested validation scenarios with Postman. |
| [Day 5](./Day5/README.md) | API Security & Hardening               | Added rate limiting, CORS, HTTPS redirection, HSTS, and reviewed the codebase for raw SQL usage.                                 |

---

## 🔗 Day Navigation

### [Day 1 — JWT Authentication & Token Issuance →](./Day1/README.md)

Implemented the authentication foundation using JWT, including login, token generation, claims, and Bearer authentication.

### [Day 2 — JWT Authentication Configuration →](./Day2/README.md)

Configured JWT Bearer authentication and integrated token validation into the ASP.NET Core middleware pipeline.

### [Day 3 — Authorization & RBAC →](./Day3/README.md)

Extended authentication with `[Authorize]`, User/Admin roles, role-based authorization, named policies, and protected CRUD operations.

### [Day 4 — Input Validation with FluentValidation →](./Day4/README.md)

Added a dedicated validation layer using FluentValidation for Create and Update Participant requests.

### [Day 5 — API Security & Hardening →](./Day5/README.md)

Hardened the API using rate limiting, CORS, HTTPS redirection, HSTS, and a review for raw SQL usage.

---

## 🔄 Week 4 Workflow

```text
JWT Authentication
        ↓
JWT Validation
        ↓
Authorization & RBAC
        ↓
Input Validation
        ↓
API Security & Hardening
```

---

## 🧠 Week 4 Skills

* JWT Authentication
* JWT Bearer
* Claims
* ASP.NET Core Identity
* Role-Based Authorization
* Policy-Based Authorization
* `[Authorize]`
* FluentValidation
* DTO Validation
* Dependency Injection
* Rate Limiting
* CORS
* HTTPS & HSTS
* SQL Injection Prevention
* Postman API Testing
* API Security

---

## 🎯 Week 4 Outcome

By the end of Week 4, the **Training Management API** was extended from a basic CRUD API into a more secure backend application.

The week connected the main security layers:

**Authentication → Authorization → Validation → API Hardening**

The final API can authenticate users with JWT, control access using roles and policies, validate incoming data, and apply additional security protections such as rate limiting, CORS, HTTPS, and HSTS.

### 🔗 Quick Links

[Day 1](./Day1/README.md) ·
[Day 2](./Day2/README.md) ·
[Day 3](./Day3/README.md) ·
[Day 4](./Day4/README.md) ·
[Day 5](./Day5/README.md)
