# Week 7 — Sprint 2: Authentication & Role-Based Access

## Overview

Week 7 focused on adding real authentication, authorization, role-based access, ownership checks, and custom middleware to the Cardiac Patient Monitoring API.

The main goal was to move the project from a functional API to a more secure API where users can only access the resources they are authorized to use.

---

## Day 1 — Sprint Planning & Identity Integration

### What We Added

* Created the Sprint 2 plan and defined the main authentication and authorization goals.
* Integrated ASP.NET Core Identity into the existing `CardiacPatientMonitoringDbContext`.
* Added `Patient` and `Admin` roles.
* Linked the `Patient` domain entity to `IdentityUser` using `UserId`.
* Added a one-to-one relationship between `Patient` and `IdentityUser`.
* Created and applied the `AddPatientIdentityLink` migration.

### Result

The existing patient data remains separate from authentication data while each registered patient can be linked to an Identity account.

---

## Day 2 — Registration, Login & JWT

### What We Added

* Implemented patient registration.
* Registration creates both:

  * an `IdentityUser`
  * a linked `Patient`
* Assigned the `Patient` role automatically to public registrations.
* Added login functionality.
* Implemented JWT token generation.
* Added domain-specific JWT claims including:

  * `patientId`
  * `role`
  * `email`
  * `sub`
  * `jti`
* Added a seeded Admin account separately from public registration.

### Result

A registered patient can log in and receive a JWT that identifies both the authenticated user and the linked patient record.

---

## Day 3 — RBAC & Ownership Authorization

### What We Added

* Applied `[Authorize]` to protected API areas.
* Applied `Admin` role restrictions to Admin-only endpoints.
* Added ownership checks using the `patientId` claim from the JWT.
* Prevented patients from accessing another patient's data.
* Applied authorization rules across:

  * Patients
  * Appointments
  * Medications
  * Vital Signs
  * Medication Orders
* Audited the authorization logic and fixed the missing RBAC/ownership checks in `VitalSignsController`.

### Example

A patient can access their own resources, but attempting to access another patient's resources returns:

```text
403 Forbidden
```

### Result

The API now checks both the user's role and ownership of the requested resource.

---

## Day 4 — Custom Middleware

### What We Added

Implemented `RequestTrackingMiddleware` to handle a genuine cross-cutting concern.

The middleware:

* Generates a unique Correlation ID for every request.
* Adds the Correlation ID to the response header as `X-Correlation-ID`.
* Measures request execution time.
* Logs:

  * Correlation ID
  * HTTP method
  * Request path
  * Status code
  * Elapsed time

The middleware was registered in the application's request pipeline.

### Result

Requests can now be traced through the application using their Correlation ID, while their execution time and status are recorded in the logs.

---

## Day 5 — Sprint Review & Integration Testing

### What We Tested

We tested the complete authentication and authorization flow using Swagger.

### Main Test Cases

| Test                                         | Expected Result |
| -------------------------------------------- | --------------- |
| Register a new patient                       | `200 OK`        |
| Login and receive JWT                        | `200 OK`        |
| Patient accesses own data                    | `200 OK`        |
| Patient accesses another patient's data      | `403 Forbidden` |
| Patient accesses Admin-only endpoint         | `403 Forbidden` |
| Admin accesses Admin endpoint                | `200 OK`        |
| Patient creates an order for themselves      | `200 OK`        |
| Patient creates an order for another patient | `403 Forbidden` |

These tests demonstrated both successful access and deliberate authorization rejections.

---

## Sprint 2 Outcome

By the end of Week 7, the Cardiac Patient Monitoring API had:

* ASP.NET Core Identity integrated with the existing database.
* Patient-to-IdentityUser linking.
* Patient and Admin roles.
* Patient registration and login.
* JWT authentication with domain-specific claims.
* Role-based authorization.
* Resource ownership checks.
* Protected patient-specific resources.
* Medication Order authorization.
* Custom request tracking middleware.
* Integration testing for successful and rejected authorization scenarios.

### What We Gained from Week 7

Week 7 added the security layer that was missing from the earlier API.

The project moved from:

**Functional API → Authenticated & Authorized API**

The most important improvement was that the API no longer only checks **who the user is**, but also checks **what role they have and whether the requested resource actually belongs to them**.

---

## Sprint 2 Retrospective

### What Went Well

* Identity was successfully integrated into the existing capstone database.
* Registration, login, and JWT authentication worked end-to-end.
* Role-based and ownership authorization were applied across the main protected resources.
* Deliberate `403 Forbidden` tests helped verify that unauthorized access was actually blocked.
* Custom middleware provided request tracing and timing across multiple endpoints.

### What We Improved

During the authorization audit, we identified missing authorization checks in `VitalSignsController` and corrected them so the authorization model was consistent with the other protected resources.

### Sprint 3 Action

For Sprint 3, we will keep an explicit authorization test checklist for every new protected resource endpoint, including:

* unauthenticated access
* patient accessing their own resource
* patient accessing another patient's resource
* Admin access
* Admin-only operations

---

## Important Note

The Week 7 implementation was tested using **Swagger** in our actual work, even though the official training material specifies Postman.

Mentor code review was part of the official Week 7 curriculum, but it was **not completed as part of our implementation**, so it is not presented as completed here.
