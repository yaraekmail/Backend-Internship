

# Week 2 - Day 2 — Advanced LINQ & Deferred Execution

## 📚 Learning Objectives

During Day 2, I practiced:

* Understanding deferred and immediate execution in LINQ.
* Using `GroupBy` to group related data.
* Using `Join` to combine related collections.
* Using `SelectMany` to flatten nested collections.
* Understanding how LINQ queries behave when the source collection changes.

---

## 🛠️ Project

**AlMandoob Stone Management System**

The Day 2 tasks were implemented using the project's existing domain models and C# collections.

The main implementation is available in [`Program.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs).

---

## 📂 Project Structure

```text
Day2
│   Day2.csproj
│   Program.cs
│   README.md
│
└── Models
    ├── Customer.cs
    ├── Employee.cs
    ├── order.cs
    ├── Person.cs
    ├── Project.cs
    ├── Service.cs
    ├── Stone.cs
    ├── StoneType.cs
    └── Supplier.cs
```

The LINQ exercises mainly work with the `Customer`, `Project`, and `Service` models.

* [`Customer.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Models/Customer.cs)
* [`Project.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Models/Project.cs)
* [`Service.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Models/Service.cs)

---

# 1. Creating Related Collections

Two collections were created in [`Program.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs):

* `List<Customer>`
* `List<Project>`

The `Project` model contains a `CustomerId`, which was used to establish the relationship between customers and projects.

Six customers were created, along with multiple projects containing:

* Project ID
* Customer ID
* Employee ID
* Total Price

---

# 2. GroupBy

Projects were grouped by `CustomerId` and the total project price for each customer was calculated.

```csharp
var totalorder = projects
    .GroupBy(p => p.CustomerId)
    .Select(group => new
    {
        CustomerId = group.Key,
        TotalPrice = group.Sum(p => p.TotalPrice)
    });
```

The query produces a result containing:

* `CustomerId`
* `TotalPrice`

This demonstrates how `GroupBy()` can organize related data into groups and how `Sum()` can be used to calculate an aggregate value for each group.

🔗 [View the GroupBy implementation in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

---

# 3. Join

The `Customer` and `Project` collections were joined using the customer ID.

```csharp
var resut = customers.Join(
    projects,
    customer => customer.Id,
    project => project.CustomerId,
    (customer, project) => new
    {
        CustomerName = customer.Name,
        ProjectId = project.Id,
        TotalPrice = project.TotalPrice
    });
```

The relationship used in the join is:

```text
Customer.Id
     ↓
Project.CustomerId
```

The resulting data contains:

* Customer name
* Project ID
* Total project price

🔗 [View the Join implementation in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

---

# 4. SelectMany

A separate collection of projects containing services was created.

Each project can contain multiple services.

For example, the implemented data includes services such as:

* External Cladding
* Stone Engraving
* Roman Columns
* Interior Decoration
* Stone Polishing

`SelectMany()` was used to flatten the nested service collections:

```csharp
var result = projects.SelectMany(p => p.Services);
```

Instead of returning a collection of service collections, the query produces one sequence containing the services directly.

🔗 [View the SelectMany implementation in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

🔗 [View Service.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Models/Service.cs)

---

# 5. Deferred Execution

Deferred execution was demonstrated using `Where()`:

```csharp
var a = projects.Where(p => p.TotalPrice > 500);
```

The query was defined first without immediately materializing its results.

A new project with a total price of `1500` was then added to the source collection.

The query was subsequently enumerated using `foreach`.

This demonstrates that the query is evaluated when it is enumerated, allowing changes to the source collection to affect the results.

🔗 [View the Deferred Execution implementation in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

---

# 6. Immediate Execution

Immediate execution was demonstrated using `ToList()`:

```csharp
var w = projects
    .Where(p => p.TotalPrice > 500)
    .ToList();
```

Calling `ToList()` materializes the query results immediately.

After the list was created, another project was added to the original `projects` collection.

The previously materialized `w` list does not automatically execute the LINQ query again.

This demonstrates the difference between deferred execution and immediate execution.

🔗 [View the Immediate Execution implementation in Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

---

# 7. LINQ Methods Practiced

The following LINQ methods were practiced during Day 2:

* `Where()`
* `Select()`
* `GroupBy()`
* `Join()`
* `SelectMany()`
* `Sum()`
* `ToList()`

All implementations are available in:

🔗 [`Program.cs`](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day2/Program.cs)

---

# 8. Hands-On Lab

The Day 2 practical requirements were implemented as follows:

* [x] Created related `Customers` and `Projects` collections.
* [x] Created six customer records.
* [x] Created projects linked to customers using `CustomerId`.
* [x] Used `GroupBy()` to calculate total project prices per customer.
* [x] Used `Join()` to combine customer and project information.
* [x] Used `SelectMany()` to flatten project services.
* [x] Demonstrated deferred execution by modifying the source collection before enumeration.
* [x] Demonstrated immediate execution using `ToList()`.

---

# 🚀 Skills Practiced

* LINQ
* `GroupBy`
* `Join`
* `SelectMany`
* `Where`
* `Select`
* `Sum`
* `ToList`
* Deferred Execution
* Immediate Execution
* Lambda Expressions
* Working with related collections
* Data transformation and aggregation

---

# 🎯 Day 2 Outcome

By the end of Day 2, I practiced advanced LINQ operations using the **AlMandoob Stone Management System** domain.

The implementation demonstrated how to:

* Group project data by customer.
* Calculate aggregate values using `Sum()`.
* Join related customer and project data.
* Flatten nested service collections.
* Understand deferred execution.
* Compare deferred execution with immediate execution using `ToList()`.

### 🔗 Day 2 Project

[View Week 2 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2)

[View Day 2 on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day2)
