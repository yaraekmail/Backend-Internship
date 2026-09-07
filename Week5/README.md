# Week 5 — Day 5: Applying Testing to the Project

## 1. Overview

Day 5 was the final day of Week 5. The goal was to apply the testing and error-handling practices learned throughout the week to the **Cardiac Patient Monitoring System**, identify the highest-risk parts of the system, run the complete test suite, and prepare the project for the next development phase.

The work focused on:

* Identifying high-risk functionality.
* Adding unit tests for important business logic.
* Using **Moq** to isolate dependencies.
* Testing authentication logic.
* Reusing integration tests created earlier in the week.
* Running the Week 5 test projects together.
* Reviewing the testing and error-handling approach before moving forward.

---

## 2. Project Used

The project used throughout Day 5 was the existing:

**Cardiac Patient Monitoring System**

Technology:

* ASP.NET Core Web API
* .NET 10
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* xUnit
* Moq
* WebApplicationFactory

Day 5 did **not** introduce a new project architecture. The existing project and the testing work from Days 1–4 were reused.

---

## 3. Day 5 Tasks

### Task 1 — Reuse the Existing Project

The existing **Cardiac Patient Monitoring System** was selected as the project for the Week 5 testing activities.

No new application was created.

---

### Task 2 — Identify the Highest-Risk Areas

Instead of trying to test every line of code, three areas were selected based on risk and importance.

| Risk Area                  | Why It Was Selected               | Testing Type    |
| -------------------------- | --------------------------------- | --------------- |
| Heart-rate calculation     | Contains actual calculation logic | Unit Test       |
| Patient service dependency | Depends on a repository           | Unit Test + Moq |
| Authentication login       | Security-sensitive functionality  | Unit Test       |

### High-Risk Area 1 — Vital Sign Calculation

The `VitalSignService` contains calculation logic for the average heart rate.

The tests verify:

* Correct average calculation.
* Empty input handling.
* Single-value input.
* Multiple input combinations using `[Theory]`.

**Code:**

[Open `VitalSignService.cs`](../Day1/CardiacPatientMonitoring.Tests/VitalSignService.cs)

**Tests:**

[Open `VitalSignServiceTests.cs`](../Day1/CardiacPatientMonitoring.Tests/VitalSignServiceTests.cs)

---

### High-Risk Area 2 — Patient Service and Repository Dependency

The `PatientService` depends on `IPatientRepository`.

For testing, the real database repository was replaced with a **Moq mock**.

The tests verify:

* A patient is returned when the repository provides one.
* Repository exceptions are propagated correctly.
* The repository is called exactly once.

The repository/interface were introduced specifically to demonstrate dependency isolation and mocking during the Week 5 training. They are **not an additional database feature or required architecture of the capstone project**.

**Service:**

[Open `PatientService.cs`](../Day2/CardiacPatientMonitoring.Tests/Services/PatientService.cs)

**Interface:**

[Open `IPatientRepository.cs`](../Day2/CardiacPatientMonitoring.Tests/Repositories/IPatientRepository.cs)

**Tests:**

[Open `PatientServiceTests.cs`](../Day2/CardiacPatientMonitoring.Tests/PatientServiceTests.cs)

---

### High-Risk Area 3 — Authentication

Authentication was selected because login is security-sensitive functionality.

The Day 5 test isolates the `AuthController` from the real Identity database by mocking:

* `UserManager<IdentityUser>`
* `SignInManager<IdentityUser>`

The test simulates:

1. Finding the user by email.
2. Validating the password.
3. Retrieving the user's role.
4. Generating the JWT.
5. Returning a successful response containing a token.

**Controller:**

[Open `AuthController.cs`](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)

**Tests:**

[Open `AuthControllerTests.cs`](../Day5/CardiacPatientMonitoring.Tests/AuthControllerTests.cs)

---

## 4. Integration Testing

The integration tests created on Day 3 were reused as part of the Week 5 testing strategy.

These tests run against the API using:

* `WebApplicationFactory`
* An in-memory Entity Framework database
* Real HTTP requests
* Real authentication flow

The integration tests cover the patient endpoint:

### Test 1 — Existing Patient

A request is sent for the seeded patient:

```text
11111111-1111-1111-1111-111111111111
```

Expected result:

```text
HTTP 200 OK
```

The response is also checked to confirm that the expected patient data is returned.

### Test 2 — Patient Not Found

A request is sent using a random patient ID.

Expected result:

```text
HTTP 404 Not Found
```

**Integration tests:**

[Open `PatientsApiIntegrationTests.cs`](../Day3/CardiacPatientMonitoring.Tests/PatientsApiIntegrationTests.cs)

**Test application factory:**

[Open `CustomWebApplicationFactory.cs`](../Day3/CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)

---

## 5. Centralized Error Handling

Centralized exception handling was implemented on Day 4 and became part of the final Week 5 setup.

The global middleware:

1. Catches unexpected exceptions.
2. Logs the complete exception on the server.
3. Prevents internal exception details from being returned to the client.
4. Returns a standardized `ProblemDetails` response.
5. Uses HTTP `500 Internal Server Error`.

**Middleware:**

[Open `GlobalExceptionMiddleware.cs`](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Middleware/GlobalExceptionMiddleware.cs)

**Middleware tests:**

[Open `GlobalExceptionMiddlewareTests.cs`](../Day4/CardiacPatientMonitoring.Tests/GlobalExceptionMiddlewareTests.cs)

A temporary `/api/test-error` endpoint was used to deliberately generate an exception and verify the middleware behavior.

The client receives a safe response instead of the real exception message or stack trace.

---

## 6. Week 5 Test Structure

The Week 5 testing work is separated by training day:

```text
Week5/
│
├── Day1/
│   └── CardiacPatientMonitoring.Tests/
│       ├── VitalSignService.cs
│       ├── VitalSignServiceTests.cs
│       └── README.md
│
├── Day2/
│   └── CardiacPatientMonitoring.Tests/
│       ├── Repositories/
│       │   ├── IPatientRepository.cs
│       │   └── PatientRepository.cs
│       ├── Services/
│       │   └── PatientService.cs
│       ├── PatientServiceTests.cs
│       └── README.md
│
├── Day3/
│   └── CardiacPatientMonitoring.Tests/
│       ├── CustomWebApplicationFactory.cs
│       ├── PatientsApiIntegrationTests.cs
│       └── README.md
│
├── Day4/
│   └── CardiacPatientMonitoring.Tests/
│       ├── CustomWebApplicationFactory.cs
│       ├── GlobalExceptionMiddlewareTests.cs
│       └── README.md
│
└── Day5/
    └── CardiacPatientMonitoring.Tests/
        ├── AuthControllerTests.cs
        └── README.md
```

---

## 7. Running the Full Week 5 Test Suite

A separate solution was created to aggregate the API project and all Week 5 test projects:

[Open `Week5Tests.slnx`](../../Week5Tests.slnx)

The solution contains:

* Cardiac Patient Monitoring API
* Day 1 tests
* Day 2 tests
* Day 3 tests
* Day 4 tests
* Day 5 tests

The complete suite can be executed with:

```powershell
dotnet test "Week5Tests.slnx"
```

This allows all Week 5 test projects to be executed through one solution instead of running each test project independently.

---

## 8. Testing Strategy Applied

The main lesson from Day 5 was **targeted testing based on risk**.

The project does not need every line of code to have a test.

Instead, testing was prioritized around functionality that can cause important failures:

### Business Logic

Example:

`CalculateAverageHeartRate()`

A wrong calculation can produce incorrect patient information.

### Dependencies

Example:

`PatientService → IPatientRepository`

Moq allows the service to be tested without depending on the real database.

### Authentication

Example:

`AuthController.Login()`

Authentication is security-sensitive, so its behavior is important to verify.

### API Behavior

Example:

`GET /api/Patients/{id}`

Integration testing verifies that multiple application components work together through a real HTTP request.

### Error Handling

Example:

`GlobalExceptionMiddleware`

The application should fail safely without exposing internal exception details to clients.

---

## 9. Day 5 Result

By the end of Day 5:

* The three highest-risk areas were identified.
* Unit tests covered the selected business logic.
* Moq was used to isolate dependencies.
* Authentication login was unit tested.
* Integration tests from Day 3 remained part of the Week 5 test suite.
* Global exception handling from Day 4 remained part of the project.
* A solution was created to run the Week 5 test projects together.
* The Week 5 testing approach was ready to carry forward into the next phase.

---

## 10. Quick Code Navigation

### Day 1 — Unit Testing

* [VitalSignService](../Day1/CardiacPatientMonitoring.Tests/VitalSignService.cs)
* [VitalSignServiceTests](../Day1/CardiacPatientMonitoring.Tests/VitalSignServiceTests.cs)

### Day 2 — Moq

* [PatientService](../Day2/CardiacPatientMonitoring.Tests/Services/PatientService.cs)
* [IPatientRepository](../Day2/CardiacPatientMonitoring.Tests/Repositories/IPatientRepository.cs)
* [PatientRepository](../Day2/CardiacPatientMonitoring.Tests/Repositories/PatientRepository.cs)
* [PatientServiceTests](../Day2/CardiacPatientMonitoring.Tests/PatientServiceTests.cs)

### Day 3 — Integration Testing

* [PatientsApiIntegrationTests](../Day3/CardiacPatientMonitoring.Tests/PatientsApiIntegrationTests.cs)
* [CustomWebApplicationFactory](../Day3/CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)

### Day 4 — Error Handling

* [GlobalExceptionMiddleware](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Middleware/GlobalExceptionMiddleware.cs)
* [GlobalExceptionMiddlewareTests](../Day4/CardiacPatientMonitoring.Tests/GlobalExceptionMiddlewareTests.cs)

### Day 5 — Authentication Testing

* [AuthController](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)
* [AuthControllerTests](../Day5/CardiacPatientMonitoring.Tests/AuthControllerTests.cs)

### Full Test Solution

* [Week5Tests.slnx](../../Week5Tests.slnx)
