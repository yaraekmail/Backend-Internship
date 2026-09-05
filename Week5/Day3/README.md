# Day 3 — Integration Testing with WebApplicationFactory

## Overview

Day 3 focused on **Integration Testing** using `WebApplicationFactory`, `HttpClient`, an **In-Memory test database**, and JWT authentication.

The goal was to test the Cardiac Patient Monitoring API through real HTTP requests while keeping the tests isolated from the real SQL Server database.

---

## What We Implemented

### 1. WebApplicationFactory

Created a custom test factory:

* [CustomWebApplicationFactory.cs](./CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)

It runs the API in a test environment and allows the tests to send HTTP requests using `HttpClient`.

The API `Program` class was also made accessible to the test project:

* [Program.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Program.cs)

---

### 2. In-Memory Test Database

The real API uses SQL Server, but the integration tests use an **EF Core In-Memory database**.

This keeps test data separate from the application's real database.

During setup, the original SQL Server registrations were removed and replaced with the In-Memory provider.

The existing seed data is then loaded into the test database.

* [SeedData.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/SeedData.cs)

---

### 3. JWT Authentication

The `PatientsController` is protected with `[Authorize]`, so the integration tests cannot access it without authentication.

The tests therefore:

1. Send a login request to `/api/Auth/login`.
2. Use the seeded Admin credentials.
3. Receive a real JWT token.
4. Add the token to the `Authorization` header.
5. Send the authenticated Patient request.

* [AuthController.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)
* [PatientsController.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/PatientsController.cs)
* [IdentitySeeder.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/IdentitySeeder.cs)

---

## Integration Tests

The tests are located in:

* [PatientsApiIntegrationTests.cs](./CardiacPatientMonitoring.Tests/PatientsApiIntegrationTests.cs)

### Existing Patient

Tests that an authenticated request for an existing patient:

* Returns `200 OK`
* Returns the expected patient ID
* Returns `John` as the first name
* Returns `Smith` as the last name

### Non-Existing Patient

Tests that an authenticated request for a patient that does not exist:

* Returns `404 Not Found`

---

## Problems Found and Fixed

### SQL Server and In-Memory Database Conflict

At first, both SQL Server and In-Memory providers were registered, which caused an EF Core error.

The factory was updated to remove the original SQL Server registrations before adding the In-Memory database.

### Unauthorized Response

The first Patient request returned:

`401 Unauthorized`

This was expected because the endpoint requires authentication.

The test was updated to perform a real login and send the returned JWT token with the request.

### Invalid Test User

The first login attempt used a test user that was not created by the current `IdentitySeeder`.

The test was corrected to use the existing Admin account:

`admin@cardiac.local`

After these changes, all integration tests passed successfully.

---

## Test Result

Final result:

```text
Test summary: total: 3, failed: 0, succeeded: 3, skipped: 0
```

All Day 3 integration tests passed successfully.

---

## Related Project Files

**Main API:**

[CardiacPatientMonitoring.Api](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/)

**Controllers:**

* [PatientsController.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/PatientsController.cs)
* [AuthController.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)

**Database & Seeding:**

* [CardiacPatientMonitoringDbContext.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)
* [SeedData.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/SeedData.cs)
* [IdentitySeeder.cs](../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/IdentitySeeder.cs)

**Day 3 Tests:**

* [PatientsApiIntegrationTests.cs](./CardiacPatientMonitoring.Tests/PatientsApiIntegrationTests.cs)
* [CustomWebApplicationFactory.cs](./CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)
