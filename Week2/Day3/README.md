

# Week 2 - Day 3 — Async/Await Deep Dive & Concurrency Basics

## 📚 Learning Objectives

During Day 3, I practiced:

* Understanding the Task-based asynchronous pattern.
* Working with `Task` and `Task<T>`.
* Using `async` and `await`.
* Running independent asynchronous operations concurrently with `Task.WhenAll`.
* Measuring sequential and concurrent execution time.
* Using `CancellationToken` to cancel an asynchronous operation.
* Avoiding blocking async code with `.Result` and `.Wait()`.

---

## 🛠️ Project

**AlMandoob Stone Management System**

The Day 3 implementation demonstrates asynchronous operations using the project's domain and service layer.

The main implementation is available in:

🔗 [`Program.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Program.cs)

---

## 📂 Project Structure

```text
Day3
│   Day3.csproj
│   Program.cs
│
├── Models
│   ├── Customer.cs
│   ├── Employee.cs
│   ├── order.cs
│   ├── Person.cs
│   ├── Project.cs
│   ├── Service.cs
│   ├── Stone.cs
│   ├── StoneType.cs
│   └── Supplier.cs
│
└── Services
    └── StoneManagementService.cs
```

The Day 3 program uses `StoneManagementService` to retrieve customers, projects, and services asynchronously.

🔗 [`StoneManagementService.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Services/StoneManagementService.cs)

---

# 1. Task 2 — Sequential Async Operations

Three asynchronous operations were executed sequentially:

```csharp
var customers = await service.GetCustomersAsync(cts.Token);
var projects = await service.GetProjectsAsync(cts.Token);
var services = await service.GetServicesAsync(cts.Token);
```

Each operation is awaited before the next operation starts.

A `Stopwatch` was used to measure the total execution time:

```csharp
Stopwatch stopwatch = new();
stopwatch.Start();

// asynchronous operations

stopwatch.Stop();
```

The program then displays the retrieved customers, projects, and services, followed by the sequential execution time.

🔗 [View Task 2 in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Program.cs)

---

# 2. Task 3 — Concurrent Operations with Task.WhenAll

The same three operations were then started before awaiting their completion:

```csharp
Task<List<Customer>> customersTask =
    service.GetCustomersAsync(cts.Token);

Task<List<Project>> projectsTask =
    service.GetProjectsAsync(cts.Token);

Task<List<Service>> servicesTask =
    service.GetServicesAsync(cts.Token);

await Task.WhenAll(
    customersTask,
    projectsTask,
    servicesTask);
```

Instead of waiting for each operation to finish before starting the next one, the independent tasks are started first and then awaited together using `Task.WhenAll`.

The execution time was measured separately to compare it with the sequential version.

🔗 [View Task 3 in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Program.cs)

---

# 3. Sequential vs. Concurrent Execution

The program compares two approaches:

### Sequential

```text
Get Customers
      ↓
Get Projects
      ↓
Get Services
```

Each operation waits for the previous one.

### Concurrent

```text
Get Customers ─┐
Get Projects  ─┼──→ Task.WhenAll
Get Services  ─┘
```

The independent operations are started together.

The program measures both approaches using `Stopwatch`:

```csharp
Console.WriteLine(
    $"Sequential Time: {stopwatch.ElapsedMilliseconds} ms");

Console.WriteLine(
    $"Concurrent Time: {stopwatch.ElapsedMilliseconds} ms");
```

🔗 [View the complete comparison in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Program.cs)

---

# 4. Async All the Way

The implementation uses `await` when calling asynchronous methods instead of blocking on a `Task`.

The approach followed is:

```csharp
var customers = await service.GetCustomersAsync(cts.Token);
```

rather than blocking with methods such as:

```csharp
.Result
.Wait()
```

This follows the async-all-the-way approach and keeps the asynchronous call chain asynchronous.

---

# 5. Task 4 — CancellationToken

Cancellation was demonstrated using `CancellationTokenSource`.

A token was passed to the asynchronous operation:

```csharp
CancellationTokenSource cts = new();

Task<List<Customer>> customersTask =
    service.GetCustomersAsync(cts.Token);
```

The program then waits for two seconds:

```csharp
await Task.Delay(2000);
```

After the delay, cancellation is requested:

```csharp
cts.Cancel();
```

The operation is awaited inside a `try` block.

If cancellation occurs, `OperationCanceledException` is caught:

```csharp
catch (OperationCanceledException)
{
    Console.WriteLine("The operation was cancelled.");
}
```

🔗 [View Task 4 in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Program.cs)

---

# 6. Service Layer

The asynchronous operations are called through:

```csharp
StoneManagementService
```

The service is instantiated in the program:

```csharp
StoneManagementService service = new();
```

The program then calls:

* `GetCustomersAsync()`
* `GetProjectsAsync()`
* `GetServicesAsync()`

with a `CancellationToken`.

🔗 [`StoneManagementService.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day3/Services/StoneManagementService.cs)

---

# 7. Hands-On Lab

The Day 3 practical requirements were implemented:

* [x] Used asynchronous methods for different data sources.
* [x] Called three asynchronous operations sequentially.
* [x] Measured sequential execution time using `Stopwatch`.
* [x] Started the three operations concurrently.
* [x] Used `Task.WhenAll` to await the concurrent operations.
* [x] Measured concurrent execution time.
* [x] Used `CancellationToken`.
* [x] Demonstrated cancellation using `CancellationTokenSource`.
* [x] Handled `OperationCanceledException`.

---

# 🚀 Skills Practiced

* `async`
* `await`
* `Task`
* `Task<T>`
* `Task.WhenAll`
* `CancellationToken`
* `CancellationTokenSource`
* `OperationCanceledException`
* `Stopwatch`
* Sequential asynchronous execution
* Concurrent asynchronous execution
* Async-all-the-way pattern

---

# 🎯 Day 3 Outcome

By the end of Day 3, I practiced asynchronous programming using the **AlMandoob Stone Management System**.

The implementation demonstrated the difference between sequential and concurrent asynchronous operations and showed how `Task.WhenAll` can be used when multiple independent operations need to run concurrently.

I also implemented cancellation using `CancellationTokenSource` and handled `OperationCanceledException` when the asynchronous operation was cancelled.

### 🔗 Day 3 Project

[View Day 3 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day3)

[View Week 2 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2)
