# Cardiac Patient Monitoring System — Day 1

## Overview

Day 1 focuses on introducing **Unit Testing with xUnit** and applying the **Arrange–Act–Assert (AAA)** testing pattern to a simple pure service method.

The testing project was created separately from the main API project and connected to it using a project reference.

---

## Project Structure

```text
Backend-Internship
├── Project
│   └── CardiacPatientMonitoring
│       └── CardiacPatientMonitoring.Api
│           └── Services
│               └── VitalSignService.cs
│
└── Week5
    └── Day1
        └── CardiacPatientMonitoring.Tests
            ├── VitalSignServiceTests.cs
            └── CardiacPatientMonitoring.Tests.csproj
```

---

## What Was Implemented

### 1. xUnit Test Project

Created a dedicated xUnit project:

**`CardiacPatientMonitoring.Tests`**

The test project targets `.NET 10` and includes:

* xUnit
* xUnit Visual Studio Runner
* Microsoft.NET.Test.Sdk
* Coverlet Collector

The test project references the main API project so it can access and test the application's code.

[View Test Project Configuration](CardiacPatientMonitoring.Tests.csproj)

---

### 2. VitalSignService

A simple pure service was added to the main API project to provide a method suitable for unit testing.

**Method:**

`CalculateAverageHeartRate()`

The method:

* Accepts a list of heart-rate readings.
* Calculates their average.
* Returns `0` when the list is empty.
* Has no database, HTTP, or external dependencies.

[View VitalSignService.cs](../../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Services/VitalSignService.cs)

---

### 3. Unit Tests with `[Fact]`

Three independent `[Fact]` tests were created to verify different scenarios:

| Test Scenario     | Input        | Expected Result |
| ----------------- | ------------ | --------------: |
| Multiple readings | `60, 70, 80` |            `70` |
| Empty list        | Empty        |             `0` |
| Single reading    | `75`         |            `75` |

[View VitalSignServiceTests.cs](VitalSignServiceTests.cs)

---

### 4. Parameterized Testing with `[Theory]`

A `[Theory]` test was added using three `[InlineData]` cases.

This allows the same test method to be executed with different input values and expected results.

| Input         | Expected Average |
| ------------- | ---------------: |
| `60, 70, 80`  |             `70` |
| `50, 60, 70`  |             `60` |
| `80, 90, 100` |             `90` |

---

## Arrange–Act–Assert (AAA)

Each unit test follows the **AAA pattern**:

### Arrange

Prepare the service and test data.

### Act

Call the method being tested.

### Assert

Verify that the actual result matches the expected result.

Example:

```csharp
// Arrange
var service = new VitalSignService();
var heartRates = new List<int> { 60, 70, 80 };

// Act
var result = service.CalculateAverageHeartRate(heartRates);

// Assert
Assert.Equal(70, result);
```

---

## Test Execution

Tests were executed using:

```powershell
dotnet test
```

The test project successfully:

1. Restored dependencies.
2. Built the API project.
3. Built the test project.
4. Discovered the xUnit tests.
5. Executed the tests.
6. Reported the test results.

### Final Result

**6 test cases passed successfully.**

```text
Test summary:
total: 6
failed: 0
succeeded: 6
skipped: 0
```

The six test runs consist of:

* 3 `[Fact]` tests
* 3 `[Theory]` cases

---

## Key Concepts Learned

* Unit testing
* xUnit
* `[Fact]`
* `[Theory]`
* `[InlineData]`
* Arrange–Act–Assert (AAA)
* Project references
* Running tests with `dotnet test`
* Testing pure methods without external dependencies

---

## Related Code

* [VitalSignService.cs](../../../Project/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Services/VitalSignService.cs)
* [VitalSignServiceTests.cs](VitalSignServiceTests.cs)
* [Test Project Configuration](CardiacPatientMonitoring.Tests.csproj)
