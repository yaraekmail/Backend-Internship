

# Day 1 — Generics & Advanced Collections

## Week 2

**Project:** AlMandoob Stone Management System
**Technology:** C# / .NET 10

---

## Overview

Day 1 focused on **Generics & Advanced Collections** in C#.

The practical work was implemented using the **AlMandoob Stone Management System** domain. The main task was to build a reusable **Generic Repository** that can work with different entity types while maintaining type safety and controlling how the collection is accessed.

---

## Learning Objectives

During this day, I practiced:

* Understanding why Generics are used.
* Creating a Generic Class.
* Applying Generic Constraints.
* Working with different collection interfaces.
* Using `IReadOnlyList<T>` to prevent direct modification of returned data.
* Building and testing a reusable Generic Repository.

---

## Project Domain

The project contains a `Models` folder with the domain entities used by the system.

The current domain models include:

* `Person`
* `Employee`
* `Customer`
* `Order`
* `Project`
* `Service`
* `Stone`
* `StoneType`
* `Supplier`

These models provide different entity types that can be used with the Generic Repository.

---

## 1. Why Generics?

Generics allow the same code to work with different types while maintaining type safety.

Instead of creating separate implementations for different entity types, one generic implementation can be reused.

For example:

```csharp
Repository<Employee>
Repository<Customer>
```

Both use the same `Repository<T>` implementation while keeping the entity type defined at compile time.

---

## 2. Generic Repository

A generic repository was implemented as:

```csharp
public class Repository<T> where T : class
```

The repository internally stores its items using:

```csharp
private List<T> _items = new List<T>();
```

This allows the repository to manage different entity types without duplicating the repository code.

---

## 3. Generic Constraint

The repository uses:

```csharp
where T : class
```

This constraint ensures that `T` must be a reference type.

The repository is intended to work with entity objects such as:

* `Employee`
* `Customer`
* `StoneType`

rather than value types such as `int`, `double`, or `bool`.

The constraint also allows the generic repository to clearly define the types it is designed to work with. ([Microsoft Learn][2])

---

## 4. Repository Methods

### Add

The `Add` method adds an item to the internal list:

```csharp
public void Add(T item)
{
    _items.Add(item);
}
```

The same method can therefore be used with different entity types.

For example, employees were added using:

```csharp
var employees = new Repository<Employee>();
```

and customers were added using:

```csharp
var customers = new Repository<Customer>();
```

---

### GetAll

The repository initially used `List<T>` as the return type.

It was changed to:

```csharp
public IReadOnlyList<T> GetAll()
{
    return _items.AsReadOnly();
}
```

Using `IReadOnlyList<T>` allows the caller to read the collection without directly modifying the repository's internal list.

This also provides indexed access and a known count while preventing collection modification through the returned interface. ([Microsoft Learn][3])

---

### Find

A `Find` method was implemented using:

```csharp
public List<T> Find(Func<T, bool> condition)
```

The method receives a condition and checks each item in the repository.

For example:

```csharp
employees.Find(employee => employee.JobTitle == "Manager");
```

The lambda expression provides the condition used to find matching employees.

---

## 5. Choosing Collection Interfaces

The implementation also covered the difference between collection interfaces.

### `IEnumerable<T>`

Used when only iteration over a sequence is required.

### `IReadOnlyList<T>`

Used when the caller needs to read the collection and access items by index without modifying it.

### `IList<T>`

Used when the caller genuinely needs to modify the collection.

The main principle is to return the **least permissive interface that satisfies the caller's needs**.

---

## 6. Testing the Repository

The Generic Repository was tested with two different domain model types.

### Employee Repository

```csharp
var employees = new Repository<Employee>();
```

Two employees were added:

* Ahmad — Manager
* Ali — Worker

The employees were then retrieved using `GetAll()`.

---

### Customer Repository

```csharp
var customers = new Repository<Customer>();
```

Two customers were added:

* Omar
* Khaled

The same `Repository<T>` implementation was reused for `Customer` without creating a separate customer repository.

This demonstrates the reusability of the Generic Repository.

---

## 7. Project Structure

The Day 1 project contains:

```text
Day1
│
├── Day1.csproj
├── Program.cs
├── README.md
├── Repository.cs
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

The `Models` folder contains the project's domain entities, while `Repository.cs` contains the reusable Generic Repository.

---

## 8. Hands-On Lab

The required Day 1 tasks were completed:

* [x] Created a generic `Repository<T>` class.
* [x] Implemented `Add`.
* [x] Implemented `GetAll`.
* [x] Implemented `Find` using a predicate.
* [x] Applied the `where T : class` constraint.
* [x] Tested the repository with two different domain model types.
* [x] Changed `GetAll()` to return `IReadOnlyList<T>`.
* [x] Verified that the returned collection cannot be directly modified through the read-only interface.

---

## Skills Practiced

* C# Generics
* Generic Classes
* Generic Constraints
* Generic Repository Pattern
* `List<T>`
* `IEnumerable<T>`
* `IReadOnlyList<T>`
* `IList<T>`
* `Func<T, bool>`
* Lambda Expressions
* Type Safety
* Reusable Code

---

## Day 1 Outcome

By the end of Day 1, a reusable **Generic Repository** was implemented for the **AlMandoob Stone Management System**.

The repository can be reused with different domain entities such as `Employee` and `Customer`, while `IReadOnlyList<T>` is used to prevent direct modification of the repository's returned collection.

This completed the Day 1 practical work on **Generics, Generic Constraints, and Collection Interfaces**.

[1]: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics%20?utm_source=chatgpt.com "Generic types and methods - C# | Microsoft Learn"
[2]: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters?utm_source=chatgpt.com "Constraints on type parameters - C# | Microsoft Learn"
[3]: https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ireadonlylist-1?view=netframework-4.8-pp&utm_source=chatgpt.com "IReadOnlyList<T> Interface (System.Collections.Generic) | Microsoft Learn"
