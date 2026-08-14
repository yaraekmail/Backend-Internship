# Day 5 — Middleware Pipeline & Dependency Injection

## Project

**AlMandoob Stone Management System**

Day 5 focused on understanding the **ASP.NET Core Middleware Pipeline** and **Dependency Injection (DI)**.

The practical work was implemented in a Web API project named **day5task**, where I created custom middleware, registered a service using the built-in DI container, and injected the service into a controller.

---

## Learning Objectives

During this day, I practiced:

* Understanding how the ASP.NET Core middleware pipeline works.
* Understanding middleware execution order.
* Creating custom middleware.
* Registering services using Dependency Injection.
* Understanding service lifetimes.
* Using interfaces with service implementations.
* Applying constructor injection in a Controller.
* Integrating middleware and DI into an ASP.NET Core Web API.

---

## Project Structure

```text
Day5
└── day5task
    │
    ├── Program.cs
    ├── day5task.csproj
    ├── day5task.http
    │
    ├── Controllers
    │   └── ProductsController.cs
    │
    ├── Middleware
    │   └── RequestLoggingMiddleware.cs
    │
    ├── Services
    │   ├── IProductService.cs
    │   └── ProductService.cs
    │
    └── Properties
        └── launchSettings.json
```

**View the complete Day 5 project:**
[Day5 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day5)

---

# 1. Middleware Pipeline

In ASP.NET Core, every incoming HTTP request passes through a **pipeline of middleware components**.

Each middleware can:

1. Inspect the incoming request.
2. Perform an operation.
3. Pass the request to the next middleware.
4. Optionally perform another operation when the response returns.

The order in which middleware is registered in `Program.cs` determines the order in which requests pass through the pipeline.

---

# 2. Custom Request Logging Middleware

A custom middleware called:

```text
RequestLoggingMiddleware
```

was created to log information about incoming requests.

The middleware records details such as:

* HTTP method
* Request path

This provides a simple example of how middleware can be used for cross-cutting concerns such as logging.

**View the middleware implementation:**
[RequestLoggingMiddleware.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/Middleware/RequestLoggingMiddleware.cs)

---

# 3. Middleware Registration

The custom middleware was registered in `Program.cs` so that incoming requests pass through it as part of the application pipeline.

**View the pipeline configuration:**
[Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/Program.cs)

The order of middleware registration is important because changing the order can change how requests are processed.

---

# 4. Dependency Injection

ASP.NET Core provides a built-in **Dependency Injection container**.

Instead of creating dependencies directly inside a controller, the required service can be registered with the DI container and then provided automatically when the controller is created.

This helps reduce tight coupling between classes and makes the application easier to maintain and test.

---

# 5. Service Interface

An interface was created for the product service:

```text
IProductService
```

The interface defines the contract that the service provides.

**View the interface:**
[IProductService.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/Services/IProductService.cs)

Using an interface allows the controller to depend on an abstraction rather than directly depending on a concrete implementation.

---

# 6. Service Implementation

The interface is implemented by:

```text
ProductService
```

This class contains the actual service logic.

**View the implementation:**
[ProductService.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/Services/ProductService.cs)

The separation between the interface and implementation demonstrates interface-based design and prepares the application for easier testing and future changes.

---

# 7. Service Lifetime

ASP.NET Core provides different service lifetimes:

| Lifetime  | Behavior                                                   |
| --------- | ---------------------------------------------------------- |
| Transient | Creates a new instance every time the service is requested |
| Scoped    | Creates one instance per HTTP request                      |
| Singleton | Creates one instance for the entire application lifetime   |

The appropriate lifetime depends on how the service should behave within the application.

For services that should share one instance during a single HTTP request, **Scoped** is commonly used.

---

# 8. Constructor Injection

The product service was injected into:

```text
ProductsController
```

through the controller's constructor.

This is called **constructor injection**.

Instead of the controller creating the service itself, the dependency is provided by the ASP.NET Core DI container.

**View the Controller:**
[ProductsController.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/Controllers/ProductsController.cs)

The general structure is:

```csharp
private readonly IProductService _productService;

public ProductsController(IProductService productService)
{
    _productService = productService;
}
```

This means the controller depends on `IProductService` rather than directly creating `ProductService`.

---

# 9. Middleware + Dependency Injection

The Day 5 project brought together two important ASP.NET Core concepts:

```text
Incoming Request
       │
       ▼
RequestLoggingMiddleware
       │
       ▼
ProductsController
       │
       ▼
IProductService
       │
       ▼
ProductService
       │
       ▼
Response
```

This demonstrates how middleware handles requests at the pipeline level while Dependency Injection provides the services required by application components.

---

# 10. Middleware Ordering

Middleware executes according to its registration order in `Program.cs`.

This is important because middleware can affect the request before it reaches the controller and can also perform work after the next middleware finishes.

For example, authentication and authorization have a meaningful order because authorization needs the authenticated user's information.

Understanding middleware ordering is therefore essential when building larger ASP.NET Core applications.

---

# 11. HTTP Testing

The project contains:

```text
day5task.http
```

which was used for working with HTTP requests during development.

**View the HTTP file:**
[day5task.http](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day5/day5task/day5task.http)

---

# Skills Practiced

During Day 5, I practiced:

* ASP.NET Core Middleware.
* Custom middleware creation.
* Middleware registration.
* Middleware execution order.
* Dependency Injection.
* Service interfaces.
* Service implementations.
* Service lifetimes.
* Constructor injection.
* Controller-service communication.
* HTTP API testing.

---

# Week 2 Synthesis

Day 5 also served as the synthesis point for the main concepts covered during Week 2.

### Day 1 — Generics

Built a reusable generic repository using:

* Generic classes.
* Generic constraints.
* `List<T>`.
* `IReadOnlyList<T>`.
* `Func<T, bool>`.

### Day 2 — LINQ

Practiced:

* `GroupBy()`
* `Join()`
* `SelectMany()`
* Deferred execution.
* Immediate execution.

### Day 3 — Async/Await

Practiced:

* `Task`.
* `async` / `await`.
* Sequential asynchronous operations.
* Concurrent operations using `Task.WhenAll()`.
* `CancellationToken`.

### Day 4 — ASP.NET Core Web API

Practiced:

* Web API project setup.
* Controllers.
* Minimal APIs.
* Routing.
* Route parameters.
* HTTP GET endpoints.

### Day 5 — Middleware & DI

Practiced:

* Middleware pipeline.
* Custom middleware.
* Dependency Injection.
* Service lifetimes.
* Constructor injection.

---

# Day 5 Outcome

By the end of Day 5, I had a better understanding of how an ASP.NET Core application processes requests and how its components communicate through Dependency Injection.

The project combined **middleware, controllers, services, interfaces, and dependency injection** into one practical Web API structure.

This completed the main technical topics of **Week 2** and prepared the project for the more advanced ASP.NET Core and Entity Framework Core work that follows.
