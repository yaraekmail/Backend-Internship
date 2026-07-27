# Day 1 - Generics & Generic Repository Implementation

# Project: AlMandoob Stone Management System

## Overview

During Day 1, I started building the foundation of **AlMandoob Stone Management System**, a real-world management system designed for a stone and marble business.

Instead of applying the concepts on simple training classes, the implementation was done using the actual project domain to simulate a real software development environment.

The main goal of this day was to understand how to create reusable and scalable code using:

- Generics
- Generic Classes
- Generic Repository Pattern
- Generic Constraints
- Collections Interfaces
- Lambda Expressions
- Predicate Functions
- OOP concepts integration

---

# Project Domain Design

Before implementing the repository, the project structure was designed based on real entities that exist in a stone management system.

The system contains different entities such as:

## Person (Base Entity)

A base abstract class that contains common properties shared between people in the system.

Example:

```csharp
public abstract class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
```

The purpose of creating `Person` as an abstract class is to avoid repeating common properties in different classes.

---

## Employee Entity

Represents employees working in the company.

Employee inherits from Person using inheritance:

```csharp
public class Employee : Person
{
    public string JobTitle { get; set; } = string.Empty;
}
```

Employee contains:

- Id
- Name
- Job Title

Examples of employees:

- Manager
- Worker
- Sales Employee
- Production Employee

---

## Customer Entity

Represents customers who deal with the company.

Customer also inherits from Person:

```csharp
public class Customer : Person
{

}
```

This allows sharing common properties while keeping the design organized.

---

# Project Structure

The project was organized following a clean structure:

```
AlMandoobStoneManagement

│ Program.cs

├── Models
│
│   ├── Person.cs
│   ├── Employee.cs
│   └── Customer.cs
│

└── Repositories

    └── Repository.cs
```

The separation between Models and Repositories makes the project easier to maintain and extend.

---

# Implementing Generic Repository

## What is Generic Repository?

A repository is a layer responsible for managing data operations.

Instead of creating different repositories:

```
EmployeeRepository
CustomerRepository
StoneTypeRepository
```

A generic repository allows creating one reusable class:

```csharp
Repository<T>
```

that can work with different entity types.

Example:

```csharp
Repository<Employee>
```

and:

```csharp
Repository<Customer>
```

using the same implementation.

---

# Creating Generic Class

Implemented:

```csharp
public class Repository<T>
```

The type parameter `T` allows the repository to work with any entity.

The repository stores objects using:

```csharp
private List<T> _items = new List<T>();
```

---

# Applying Generic Constraint

Added:

```csharp
where T : class
```

The constraint ensures that the repository only accepts reference types.

Reason:

The repository is designed to store entity objects such as:

- Employee
- Customer
- StoneType

and should not be used with value types like:

- int
- double
- bool

Implementation:

```csharp
public class Repository<T> where T : class
```

---

# Repository Operations

## 1. Add Method

Implemented adding new objects to the repository.

```csharp
public void Add(T item)
{
    _items.Add(item);
}
```

Example:

```csharp
employees.Add(new Employee
{
    Id = 1,
    Name = "Ahmad",
    JobTitle = "Manager"
});
```

---

# 2. GetAll Method

The first implementation returned:

```csharp
List<T>
```

However, returning the original list allows external code to modify internal data.

Example problem:

```csharp
employees.GetAll().Add(new Employee());
```

This directly changes repository data.

To solve this problem, the return type was changed to:

```csharp
IReadOnlyList<T>
```

Implementation:

```csharp
public IReadOnlyList<T> GetAll()
{
    return _items.AsReadOnly();
}
```

Benefits:

- Users can read the data.
- Users cannot add or remove items directly.
- Protects repository data.

---

# 3. Find Method Using Predicate

Implemented a dynamic filtering method:

```csharp
public List<T> Find(Func<T, bool> condition)
```

The method receives a condition using:

```csharp
Func<T,bool>
```

This allows passing different search conditions.

Example:

Find employees who are managers:

```csharp
employees.Find(employee => employee.JobTitle == "Manager");
```

The repository checks every item and returns only matching results.

---

# Testing Generic Repository

The repository was tested using multiple entity types.

## Employee Repository

```csharp
Repository<Employee>
```

Added employees and retrieved them successfully.

Example output:

```
Ahmad
Manager

Ali
Worker
```

---

## Customer Repository

```csharp
Repository<Customer>
```

The same repository was reused with Customer without creating a new repository class.

This confirms that Generics provide reusable and flexible code.

---

# Concepts Applied From OOP

During this implementation, previous OOP concepts were integrated:

## Encapsulation

The list is private:

```csharp
private List<T> _items;
```

External code cannot access it directly.

---

## Inheritance

Implemented:

```
Person
   |
   ├── Employee
   |
   └── Customer
```

Shared properties were placed in the parent class.

---

## Abstraction

Used:

```csharp
abstract class Person
```

to represent a common concept without creating direct objects from it.

---

# Skills Practiced Today

During Day 1, I practiced:

✅ Creating a real C# project structure  
✅ Designing domain entities  
✅ Applying inheritance and abstraction  
✅ Creating Generic Classes  
✅ Building Generic Repository Pattern  
✅ Using Collections (`List<T>`)  
✅ Using `Func<T,bool>` and Lambda Expressions  
✅ Applying Generic Constraints  
✅ Protecting data using `IReadOnlyList<T>`  
✅ Testing reusable code with multiple entities  

---

# Day 1 Outcome

At the end of Day 1, the foundation of **AlMandoob Stone Management System** was created.

A reusable Generic Repository was implemented successfully and connected with real project entities such as:

- Employee
- Customer

This foundation will allow adding future entities and features such as:

- Stone Types
- Orders
- Projects
- Customers Management
- Export Operations
- Production Tracking

without rewriting the same data management logic.