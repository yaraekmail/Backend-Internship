# Week 2 — Backend Development with C#

## Overview

During Week 2, I moved from basic C# and OOP concepts into more advanced backend development topics.

The week focused on building reusable code, querying collections efficiently, working with asynchronous operations, creating ASP.NET Core Web APIs, and understanding Middleware and Dependency Injection.

The practical work was mainly implemented using the **AlMandoob Stone Management System** domain, allowing the concepts to be applied to a realistic business scenario.

---

# Week 2 Learning Objectives

During this week, I practiced:

* Building reusable code using Generics.
* Working with generic repositories and constraints.
* Using advanced LINQ operations.
* Understanding deferred and immediate execution.
* Working with asynchronous programming using `async` and `await`.
* Running independent operations concurrently.
* Using cancellation tokens.
* Creating ASP.NET Core Web APIs.
* Working with Controllers and Minimal APIs.
* Defining routes and route parameters.
* Understanding the ASP.NET Core Middleware Pipeline.
* Applying Dependency Injection.
* Working with service interfaces and implementations.
* Understanding service lifetimes.
* Using constructor injection.

---

# Project Domain

The practical exercises were based on the **AlMandoob Stone Management System**, a management system for a stone and marble business.

The domain contains entities such as:

* Customers
* Employees
* Projects
* Services
* Stones
* Stone Types
* Suppliers
* Orders

The same domain was reused across several exercises to connect the different concepts learned throughout the week.

---

# Week 2 Structure

```text
Week2
│
├── AlMandoobStoneManagement
│
├── Day1
│
├── Day2
│
├── Day3
│
├── Day4
│
└── Day5
    └── day5task
```

The complete Week 2 implementation is available on [GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2).

---

# Day 1 — Generics & Generic Repository

## Focus

Day 1 focused on **Generics, Generic Classes, Generic Constraints, and Collection Interfaces**.

A reusable `Repository<T>` was implemented instead of creating separate repositories for each entity.

For example:

```text
Repository<Employee>
Repository<Customer>
```

Both use the same generic repository implementation.

### Main Concepts

* Generic classes.
* Generic type parameters.
* Generic constraints.
* `List<T>`.
* `IReadOnlyList<T>`.
* `Func<T, bool>`.
* Lambda expressions.
* Generic Repository Pattern.

The repository included:

* `Add()`
* `GetAll()`
* `Find()`

The `where T : class` constraint was also applied to restrict the repository to reference types.

The project was tested with different domain entities such as `Employee` and `Customer`.

**View Day 1:** [Day1 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day1)

---

# Day 2 — Advanced LINQ & Deferred Execution

## Focus

Day 2 focused on using LINQ to query and reshape collections.

The exercises used related `Customer` and `Project` collections.

### Main LINQ Operations

#### GroupBy

Projects were grouped according to their `CustomerId`, and the total project value for each customer was calculated.

#### Join

Customers and projects were joined using their related IDs to produce combined results containing customer and project information.

#### SelectMany

Nested project services were flattened into a single sequence using `SelectMany()`.

#### Deferred Execution

A LINQ query was created first and the source collection was modified before the query was enumerated.

This demonstrated that deferred queries are evaluated when they are actually enumerated.

#### Immediate Execution

`ToList()` was used to materialize the query immediately.

This demonstrated the difference between keeping a query deferred and storing its current results in a list.

### LINQ Methods Practiced

* `Where()`
* `Select()`
* `GroupBy()`
* `Join()`
* `SelectMany()`
* `Sum()`
* `ToList()`

**View Day 2:** [Day2 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day2)

---

# Day 3 — Async/Await & Concurrency

## Focus

Day 3 focused on asynchronous programming and concurrency using C#.

The practical implementation used `StoneManagementService` to simulate retrieving different types of data.

Three operations were used:

* Customers
* Projects
* Services

### Sequential Execution

The operations were awaited one after another:

```text
Get Customers
      ↓
Get Projects
      ↓
Get Services
```

The execution time was measured using `Stopwatch`.

### Concurrent Execution

The three tasks were started first and then awaited together using:

```text
Task.WhenAll()
```

This allowed the independent operations to execute concurrently.

The execution time was measured again to compare it with the sequential approach.

### Cancellation

A `CancellationTokenSource` was used to demonstrate cancelling an asynchronous operation.

The token was passed to the service method, and the operation was cancelled using:

```text
cts.Cancel()
```

The cancellation was handled using:

```text
OperationCanceledException
```

### Main Concepts

* `Task`
* `Task<T>`
* `async`
* `await`
* `Task.WhenAll()`
* `Stopwatch`
* `CancellationToken`
* `CancellationTokenSource`
* `OperationCanceledException`
* Sequential vs concurrent execution

**View Day 3:** [Day3 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day3)

---

# Day 4 — ASP.NET Core Project Setup & Routing

## Focus

Day 4 introduced **ASP.NET Core Web API** development.

A Web API project called `StoneApiPractice` was created and used to practice routing and API endpoints.

### Main Concepts

* ASP.NET Core Web API.
* Minimal hosting model.
* `Program.cs`.
* Controllers.
* Minimal APIs.
* Routing.
* Route parameters.
* HTTP GET.

A `Book` model was created and a `BooksController` was used to create book-related endpoints.

The API included GET endpoints for:

```text
GET /api/books
GET /api/books/{id}
```

The second endpoint demonstrated using a route parameter to retrieve a specific item.

Minimal API endpoints were also implemented in `Program.cs` to compare the approach with Controllers.

### Project Structure

```text
Day4
│
├── Documentation
│   └── Day4_API_Documentation_Improved.docx
│
└── StoneApiPractice
    │
    ├── Program.cs
    ├── StoneApiPractice.csproj
    ├── StoneApiPractice.http
    │
    ├── Controllers
    │   └── BooksController.cs
    │
    ├── Models
    │   └── Book.cs
    │
    └── Properties
        └── launchSettings.json
```

**View Day 4:** [Day4 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day4)

---

# Day 5 — Middleware & Dependency Injection

## Focus

Day 5 focused on the **ASP.NET Core Middleware Pipeline** and **Dependency Injection**.

A project called `day5task` was used to implement the concepts.

### Middleware

A custom middleware called:

```text
RequestLoggingMiddleware
```

was created to log information about incoming requests.

The middleware was registered in `Program.cs` and became part of the application's request pipeline.

### Dependency Injection

An interface and service implementation were created:

```text
IProductService
ProductService
```

The service was registered with the built-in ASP.NET Core Dependency Injection container.

### Constructor Injection

`IProductService` was injected into:

```text
ProductsController
```

through its constructor.

This allows the controller to depend on the interface rather than directly creating the service implementation.

### Service Lifetimes

The main ASP.NET Core service lifetimes studied were:

| Lifetime  | Description                                 |
| --------- | ------------------------------------------- |
| Transient | New instance every time it is requested     |
| Scoped    | One instance per HTTP request               |
| Singleton | One instance for the application's lifetime |

### Main Concepts

* Middleware Pipeline.
* Custom Middleware.
* Middleware Ordering.
* Dependency Injection.
* Interfaces.
* Service Implementations.
* Service Lifetimes.
* Constructor Injection.

**View Day 5:** [Day5 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day5)

---

# Week 2 Skills Summary

| Day   | Main Topics                                                                |
| ----- | -------------------------------------------------------------------------- |
| Day 1 | Generics, Generic Repository, Constraints, Collection Interfaces           |
| Day 2 | LINQ, GroupBy, Join, SelectMany, Deferred & Immediate Execution            |
| Day 3 | Async/Await, Tasks, Task.WhenAll, Cancellation                             |
| Day 4 | ASP.NET Core Web API, Controllers, Minimal APIs, Routing                   |
| Day 5 | Middleware, Dependency Injection, Service Lifetimes, Constructor Injection |

---

# Week 2 Synthesis

Throughout Week 2, the concepts gradually moved from reusable C# code toward real ASP.NET Core backend development.

The progression was:

```text
Generics
   ↓
LINQ
   ↓
Async/Await & Concurrency
   ↓
ASP.NET Core Web API
   ↓
Middleware & Dependency Injection
```

The first part of the week focused on writing reusable and efficient C# code.

The second part moved into building Web APIs and understanding how ASP.NET Core handles requests and manages application dependencies.

---

# Technologies & Tools

* C#
* .NET 10
* .NET SDK
* ASP.NET Core
* LINQ
* Async/Await
* Visual Studio Code
* Postman
* Git
* GitHub

---

# Week 2 Outcome

By the end of Week 2, I had progressed from advanced C# programming concepts into practical ASP.NET Core backend development.

I implemented a generic repository, practiced advanced LINQ operations, worked with asynchronous and concurrent operations, created Web API endpoints, and implemented Middleware and Dependency Injection.

These concepts provide the foundation for the database and Entity Framework Core work covered in the following week.

---

## GitHub Repository

The complete Week 2 work is available here:

**[Backend Internship — Week 2](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2)**
