# Week 5 – Day 2: Mocking with Moq

## Overview

Day 2 focused on **unit testing with mocked dependencies** using **xUnit** and **Moq**.

The goal was to test `PatientService` independently from the real database by replacing the repository dependency with a mock.

---

## What We Implemented

### 1. Repository Interface

Created [`IPatientRepository.cs`](./Repositories/IPatientRepository.cs) to define the operation required to retrieve a patient by ID.

The interface allows `PatientService` to depend on an abstraction instead of directly depending on the database.

### 2. Real Repository

Created [`PatientRepository.cs`](./Repositories/PatientRepository.cs).

It implements `IPatientRepository` and uses `CardiacPatientMonitoringDbContext` to retrieve a patient from the existing database.

### 3. Patient Service

Created [`PatientService.cs`](./Services/PatientService.cs).

The service receives `IPatientRepository` through dependency injection and uses it to retrieve a patient.

This separation makes the service easy to test without accessing the real database.

### 4. Moq Unit Tests

Created [`PatientServiceTests.cs`](./PatientServiceTests.cs) using **xUnit** and **Moq**.

The tests cover:

* **Returning a patient:** verifies that the service returns the patient provided by the mocked repository.
* **Handling a repository exception:** simulates a repository failure and verifies that the expected exception is raised.
* **Verifying interaction:** confirms that `GetByIdAsync()` is called exactly once with the expected patient ID.

---

## Testing Approach

The tests follow the **Arrange – Act – Assert (AAA)** pattern:

```text
Arrange → Create the mock and configure its behavior
Act     → Call PatientService
Assert  → Verify the result or expected behavior
```

The real repository and database are not used during these unit tests. Moq provides the required repository behavior instead.

---

## Technologies

* C#
* .NET 10
* xUnit
* Moq
* Entity Framework Core

---

## Project Structure

```text
Day2
└── CardiacPatientMonitoring.Tests
    ├── Repositories
    │   ├── IPatientRepository.cs
    │   └── PatientRepository.cs
    ├── Services
    │   └── PatientService.cs
    ├── CardiacPatientMonitoring.Tests.csproj
    ├── PatientServiceTests.cs
    └── README.md
```

> `bin` and `obj` are generated build folders and are not part of the Day 2 source files.

---

## Key Learning

The main idea of Day 2 was to **isolate the service from its dependency**.

Instead of testing against the real database, the repository is replaced with a mock. This makes the unit test faster, more controlled, and focused only on the behavior of `PatientService`.

---

## Related Code

* [IPatientRepository](./Repositories/IPatientRepository.cs)
* [PatientRepository](./Repositories/PatientRepository.cs)
* [PatientService](./Services/PatientService.cs)
* [PatientServiceTests](./PatientServiceTests.cs)
