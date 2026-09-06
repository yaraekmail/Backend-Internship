# Day 4 — Centralized Error Handling & Global Exception Middleware

## 1. Overview

Day 4 focused on implementing centralized error handling in the **Cardiac Patient Monitoring System**.

The main goal was to avoid handling unexpected exceptions separately inside every controller or endpoint. Instead, a single global middleware was added to catch unhandled exceptions, log the full exception details on the server, and return a safe and standardized `ProblemDetails` response to the client.

---

## 2. Day 4 Hands-On Requirements

The Day 4 hands-on tasks were:

1. Implement global exception-handling middleware that returns a `ProblemDetails` response for unhandled exceptions.
2. Confirm that the actual exception message and stack trace are not exposed to the client.
3. Add structured logging using `ILogger`, including request context such as the request path.
4. Deliberately trigger an unhandled exception using a test endpoint and confirm that the middleware catches it.
5. Remove redundant `try/catch` blocks from endpoints covered by the global handler.

---

## 3. Problem With Scattered Error Handling

A common approach is to put a `try/catch` block inside every controller action.

This can cause:

* Repeated error-handling code.
* Different error responses between endpoints.
* More code inside controllers.
* Inconsistent handling of unexpected exceptions.

Instead of repeating the same logic in every endpoint, we implemented one global middleware that handles unexpected exceptions for the API.

---

## 4. Global Exception Middleware

A new middleware file was created:

[GlobalExceptionMiddleware.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Middleware/GlobalExceptionMiddleware.cs)

The middleware wraps the rest of the request pipeline inside a `try/catch`.

The main flow is:

```text
HTTP Request
    ↓
Global Exception Middleware
    ↓
Controller / Endpoint
    ↓
Other API Components
    ↓
Exception
    ↑
Global Exception Middleware
    ↓
ILogger
    ↓
Server Log
    ↓
ProblemDetails
    ↓
HTTP 500 Response
    ↓
Client
```

The middleware uses `ILogger<GlobalExceptionMiddleware>` to log the complete exception details on the server.

It then creates a `ProblemDetails` response containing:

* `title`
* `status`

The client receives a generic error message instead of the internal exception details.

---

## 5. Registering the Middleware

The middleware was registered in:

[Program.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Program.cs)

It was added before authentication and authorization:

```csharp
// Handles unexpected exceptions before they reach the client.
app.UseMiddleware<GlobalExceptionMiddleware>();

// Handles JWT authentication.
app.UseAuthentication();

// Handles authorization and role checks.
app.UseAuthorization();
```

Placing the middleware in the request pipeline allows it to catch exceptions that occur later in the pipeline.

---

## 6. ProblemDetails Response

The middleware returns a standardized `ProblemDetails` response when an unexpected exception occurs.

For the test exception, Swagger showed:

```json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

This means the client receives:

* HTTP status `500`
* A safe generic error title

The actual exception message and stack trace are not returned to the client.

This is important because returning internal exception information could expose implementation details of the application.

---

## 7. Structured Logging With ILogger

The middleware uses `ILogger` to record the exception on the server.

The logging statement is structured:

```csharp
_logger.LogError(
    ex,
    "Unhandled exception while processing request {RequestPath}",
    context.Request.Path);
```

The request path is provided as a named structured logging value:

```text
{RequestPath}
```

For the test request, the terminal showed a log similar to:

```text
fail: CardiacPatientMonitoring.Api.Middleware.GlobalExceptionMiddleware[0]
      Unhandled exception while processing request /api/test-error

      System.Exception: This is a test exception.
```

The full exception and stack trace were therefore available in the server log, while the client only received the safe `ProblemDetails` response.

### Client vs Server

```text
Client
  ↓
Safe ProblemDetails
  ↓
500 Internal Server Error

Server Log
  ↓
Full Exception
  ↓
Stack Trace
  ↓
Request Path
```

This keeps detailed debugging information on the server instead of exposing it through the API response.

---

## 8. Test Error Endpoint

To deliberately trigger an unexpected exception, a temporary test endpoint was added to:

[Program.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Program.cs)

```csharp
// Test endpoint that deliberately throws an exception.
// This is temporary and will be removed after testing.
app.MapGet("/api/test-error", () =>
{
    throw new Exception("This is a test exception.");
});
```

The endpoint was used only to verify the global exception middleware.

When calling:

```text
GET /api/test-error
```

the endpoint deliberately throws an exception.

The exception is then caught by `GlobalExceptionMiddleware`.

---

## 9. Manual Verification Using Swagger

The `/api/test-error` endpoint was tested through Swagger.

The response was:

```text
Error: Internal Server Error

Response body

{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

This confirmed that:

* The exception was caught by the global middleware.
* The API returned HTTP `500`.
* The generic `ProblemDetails` response was returned.
* The real exception message was not exposed.
* The stack trace was not exposed.

---

## 10. Integration Test

An integration test was created to automatically verify the global exception handling.

Test file:

[GlobalExceptionMiddlewareTests.cs](../../Week5/Day4/CardiacPatientMonitoring.Tests/GlobalExceptionMiddlewareTests.cs)

The test sends a request to:

```text
/api/test-error
```

and checks that:

* The response status is `500`.
* The actual exception message is not included.
* `System.Exception` is not included in the response.
* The expected `ProblemDetails` title is included.
* The `500` status is included in the response.

---

## 11. Test Database Setup

The Day 4 integration test uses a separate In-Memory database.

Test factory:

[CustomWebApplicationFactory.cs](../../Week5/Day4/CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)

The test environment replaces the normal SQL Server database configuration with an In-Memory database.

This allows the integration test application to run without using the normal SQL Server database.

---

## 12. Problem We Encountered During Testing

The first `dotnet test` attempt failed.

The error was:

```text
Services for database providers
'Microsoft.EntityFrameworkCore.SqlServer',
'Microsoft.EntityFrameworkCore.InMemory'
have been registered in the service provider.
Only a single database provider can be registered in a service provider.
```

### Why did this happen?

The test application was registering:

```text
SQL Server
+
In-Memory
```

at the same time.

Entity Framework Core requires the test application to use one database provider for the DbContext.

The error appeared while the application's `IdentitySeeder` was running, before the test could reach the `/api/test-error` endpoint.

### What did we change?

We updated:

[CustomWebApplicationFactory.cs](../../Week5/Day4/CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)

and removed the remaining SQL Server DbContext configuration before registering the In-Memory provider.

The important removal was:

```csharp
// Removes the SQL Server provider configuration.
services.RemoveAll<IDbContextOptionsConfiguration<CardiacPatientMonitoringDbContext>>();
```

After that, the test application used the In-Memory provider correctly.

---

## 13. Final Test Result

After fixing the database provider conflict, `dotnet test` was run again.

The final result was:

```text
Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0

Build succeeded
```

Therefore:

```text
2 tests passed
0 tests failed
```

The test output also confirmed that the test application started successfully with the In-Memory database and that the global exception middleware logged the deliberately triggered exception.

---

## 14. Checking for Redundant try/catch Blocks

The API source code was searched for `try` and `catch` blocks.

The only `try/catch` found was inside:

[GlobalExceptionMiddleware.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Middleware/GlobalExceptionMiddleware.cs)

There were no redundant `try/catch` blocks in the controllers that needed to be removed.

This satisfies the Day 4 requirement of checking the endpoints covered by the global exception handler.

---

## 15. Files Added or Updated

### API

* [GlobalExceptionMiddleware.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Middleware/GlobalExceptionMiddleware.cs)
* [Program.cs](../../project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Program.cs)

### Day 4 Tests

* [GlobalExceptionMiddlewareTests.cs](../../Week5/Day4/CardiacPatientMonitoring.Tests/GlobalExceptionMiddlewareTests.cs)
* [CustomWebApplicationFactory.cs](../../Week5/Day4/CardiacPatientMonitoring.Tests/CustomWebApplicationFactory.cs)
* [CardiacPatientMonitoring.Tests.csproj](../../Week5/Day4/CardiacPatientMonitoring.Tests/CardiacPatientMonitoring.Tests.csproj)

---

## 16. Day 4 Result

Day 4 implemented centralized handling for unexpected exceptions using:

* ASP.NET Core Middleware
* `ProblemDetails`
* `ILogger`
* Structured logging
* Integration testing with `WebApplicationFactory`
* An In-Memory database for the test environment

The implementation was manually verified through Swagger and the terminal, and the automated integration test completed successfully with:

```text
2 passed
0 failed
```
