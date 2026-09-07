# Week 5 — Day 5

## Applying Testing to the Cardiac Patient Monitoring System

### Overview

Day 5 focused on applying the testing and error-handling practices learned during Week 5 to the **Cardiac Patient Monitoring System**.

The main goal was not to test every line of the application. Instead, the work focused on:

* identifying high-risk parts of the system
* applying unit testing to important business logic
* applying mocking to isolate service dependencies
* using integration tests for important API endpoints
* running the complete Week 5 test suite
* preparing the project for the next development phase

---

## 1. Project Context

The project is an ASP.NET Core Web API for managing cardiac patient monitoring data.

The API includes functionality for:

* Patients
* Vital Signs
* Medications
* Appointments
* Medical Conditions
* Allergies
* User Authentication and Authorization

The project uses ASP.NET Core, Entity Framework Core, SQL Server, ASP.NET Core Identity, JWT authentication, xUnit, and Moq.

---

# 2. Day 5 Tasks

Day 5 consisted of five main tasks:

1. Reuse the existing project skeleton.
2. Identify three high-risk areas and test them.
3. Have at least two integration tests for an important endpoint.
4. Run the full test suite and confirm that all tests pass.
5. Summarize the work completed during Week 5.

---

# 3. High-Risk Areas Tested

Testing was prioritized based on risk rather than attempting to test every simple property or pass-through operation.

Three high-risk areas were selected:

| Area                                   | Risk                                                            | Testing Approach |
| -------------------------------------- | --------------------------------------------------------------- | ---------------- |
| Vital sign calculation                 | Incorrect calculations can produce incorrect monitoring results | xUnit Unit Tests |
| Patient service/repository interaction | Service depends on external data access                         | xUnit + Moq      |
| Authentication / Login                 | Security-critical functionality                                 | xUnit Unit Test  |

---

## 3.1 Vital Sign Average Calculation

### Code Under Test

The `VitalSignService` contains the following calculation:

```text
CardiacPatientMonitoring.Api
└── Services
    └── VitalSignService.cs
```

The tested method is:

```text
CalculateAverageHeartRate()
```

### What Was Tested

The Day 1 test suite verifies:

* the average of multiple heart-rate readings
* an empty collection
* a single reading
* multiple input combinations using `[Theory]`

### Tests

```text
Week5
└── Day1
    └── CardiacPatientMonitoring.Tests
        └── VitalSignServiceTests.cs
```

The tests use xUnit's:

* `[Fact]`
* `[Theory]`
* `[InlineData]`

This provides direct coverage of the calculation logic without requiring the API or database.

---

# 3.2 Patient Service with Moq

### Code Under Test

The service communicates with a patient repository through an interface:

```text
Week5
└── Day2
    └── CardiacPatientMonitoring.Tests
        ├── Repositories
        │   ├── IPatientRepository.cs
        │   └── PatientRepository.cs
        ├── Services
        │   └── PatientService.cs
        └── PatientServiceTests.cs
```

### Testing Approach

The repository was replaced with a **Moq mock** during testing.

This allowed the test to verify the service logic without depending on the real database.

The tests cover:

1. returning an existing patient
2. propagating a repository exception
3. verifying that the repository is called exactly once

### Important Note

The repository and interface were introduced specifically to demonstrate dependency isolation and mocking during the Week 5 training exercise.

They are not being presented as an additional architectural requirement of the Cardiac Patient Monitoring System.

---

# 3.3 Authentication Login

Authentication was selected as the third high-risk area because login directly affects access to protected API functionality.

### Code Under Test

```text
project
└── CardiacPatientMonitoring
    └── CardiacPatientMonitoring.Api
        └── Controllers
            └── AuthController.cs
```

The tested operation is:

```text
POST /api/Auth/login
```

### Unit Test

```text
Week5
└── Day5
    └── CardiacPatientMonitoring.Tests
        └── AuthControllerTests.cs
```

The test isolates the controller from the real Identity database by mocking:

* `UserManager<IdentityUser>`
* `SignInManager<IdentityUser>`

The test configures the mocked dependencies to simulate:

1. a valid user being found
2. a valid password
3. the user having the `User` role
4. valid JWT configuration

The controller's real login logic then executes and returns an `OkObjectResult` containing a JWT token.

### Why Mocking Is Used

The purpose of this unit test is to test the controller's login logic itself.

It does not need to connect to the real Identity database or perform real password validation.

Those external dependencies are therefore replaced with controlled mocks.

---

# 4. Integration Testing

Integration testing was implemented earlier in Week 5 Day 3 and reused as part of the Day 5 project testing requirements.

### Integration Test Project

```text
Week5
└── Day3
    └── CardiacPatientMonitoring.Tests
        ├── CustomWebApplicationFactory.cs
        └── PatientsApiIntegrationTests.cs
```

### Testing Environment

`WebApplicationFactory` is used to run the API in a test environment.

For integration testing, the SQL Server database is replaced with an Entity Framework Core in-memory database.

This allows the test to exercise the actual HTTP/API pipeline without using the application's real database.

### Authentication

The integration test logs in using the seeded admin account and uses the returned JWT token for an authenticated request.

### Integration Scenarios

At least two important patient endpoint scenarios were tested:

#### Existing Patient

```text
GET /api/Patients/{existingPatientId}
```

Expected result:

```text
200 OK
```

The test also verifies the returned patient data.

#### Non-Existing Patient

```text
GET /api/Patients/{randomPatientId}
```

Expected result:

```text
404 Not Found
```

These tests verify actual HTTP endpoint behavior rather than testing individual methods in isolation.

---

# 5. Centralized Error Handling

Global exception handling was implemented during Day 4 and remains part of the project's testing baseline.

### Middleware

```text
project
└── CardiacPatientMonitoring
    └── CardiacPatientMonitoring.Api
        └── Middleware
            └── GlobalExceptionMiddleware.cs
```

The middleware:

1. catches unexpected exceptions
2. logs the full exception on the server
3. returns HTTP `500`
4. returns a standardized `ProblemDetails` response
5. prevents internal exception details from being exposed to the client

### Error Response

The client receives a safe response similar to:

```json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

The actual exception details remain in the server-side logs.

### Middleware Test

```text
Week5
└── Day4
    └── CardiacPatientMonitoring.Tests
        └── GlobalExceptionMiddlewareTests.cs
```

The test verifies that:

* HTTP 500 is returned
* the original exception message is not exposed
* the stack trace is not exposed
* the expected ProblemDetails information is returned

---

# 6. Full Week 5 Test Suite

To satisfy the full-suite testing requirement, a dedicated solution was created:

```text
Week5Tests.slnx
```

It contains:

```text
Week5Tests.slnx
├── CardiacPatientMonitoring.Api
├── Day1 Tests
├── Day2 Tests
├── Day3 Tests
├── Day4 Tests
└── Day5 Tests
```

This allows the Week 5 test projects to be executed together instead of running each test project independently.

### Full Test Command

```powershell
dotnet test "Week5Tests.slnx"
```

This command runs the test projects included in the Week 5 solution as one test suite.

### Result

The complete Week 5 test suite passed successfully with:

```text
Failed: 0
```

The individual test projects also completed successfully during verification.

---

# 7. Week 5 Testing Structure

The final testing structure is:

```text
Week5
│
├── Day1
│   └── CardiacPatientMonitoring.Tests
│       └── VitalSignServiceTests.cs
│
├── Day2
│   └── CardiacPatientMonitoring.Tests
│       ├── Repositories
│       │   ├── IPatientRepository.cs
│       │   └── PatientRepository.cs
│       ├── Services
│       │   └── PatientService.cs
│       └── PatientServiceTests.cs
│
├── Day3
│   └── CardiacPatientMonitoring.Tests
│       ├── CustomWebApplicationFactory.cs
│       └── PatientsApiIntegrationTests.cs
│
├── Day4
│   └── CardiacPatientMonitoring.Tests
│       ├── CustomWebApplicationFactory.cs
│       └── GlobalExceptionMiddlewareTests.cs
│
└── Day5
    └── CardiacPatientMonitoring.Tests
        └── AuthControllerTests.cs
```

---

# 8. Testing Approach Learned During Week 5

The Week 5 implementation established three complementary testing levels:

### Unit Testing

Tests individual pieces of logic in isolation.

```text
VitalSignService
        ↓
Unit Test
```

### Mock-Based Unit Testing

Tests a service while replacing its external dependency with a mock.

```text
PatientService
      ↓
Mock Repository
```

### Integration Testing

Tests the API through real HTTP requests using a test host and in-memory database.

```text
HTTP Request
      ↓
API Pipeline
      ↓
Controller
      ↓
Database
```

These approaches are used for different risks and should not be treated as interchangeable.

---


# 9. Result

Day 5 completed the practical testing phase of Week 5.

The Cardiac Patient Monitoring System now has:

* targeted unit tests for important logic
* mocked dependency testing
* API integration tests
* centralized exception handling
* safe ProblemDetails error responses
* a combined Week 5 test solution
* a repeatable full-suite test command

The project is ready to move from the Week 5 testing and synthesis phase toward the next development phase.
