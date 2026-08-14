
# Week 1 - Day 3 | C# Fundamentals II

> Practicing Object-Oriented Programming in C# through a small Library Management domain.

---

## 📚 Table of Contents

- [Overview](#-overview)
- [Topics Covered](#-topics-covered)
- [Domain Model](#-domain-model)
- [Hands-On Lab](#-hands-on-lab)
- [Implementation](#-implementation)
- [Technologies Used](#-technologies-used)
- [Project Structure](#-project-structure)
- [Build and Run](#-build-and-run)
- [Learning Outcomes](#-learning-outcomes)
- [Quick Links](#-quick-links)

---

## 📌 Overview

Day 3 focused on the fundamentals of Object-Oriented Programming (OOP) in C#.

The practical work was implemented as a small **Library Management System** domain.

The tasks covered:

- Creating and using classes.
- Working with objects and properties.
- Creating records for immutable data transfer objects.
- Using constructors with records and classes.
- Working with interfaces.
- Demonstrating polymorphism through an interface-based method.
- Organizing domain models into separate files.

---

## 📖 Topics Covered

### Classes, Records, and Structs

The lesson introduced the differences between:

- Classes
- Records
- Structs

A **class** is commonly used for objects that have identity, state, and behavior.

A **record** is designed for data-oriented objects and supports immutable data and value-based equality.

A **struct** is a value type that is suitable for small data structures.

The practical implementation used **classes and records**.

---

### Encapsulation and Access Modifiers

The lesson covered encapsulation and access modifiers, including:

- `public`
- `private`
- `protected`
- `internal`

The purpose of encapsulation is to control what a class exposes and how its internal state is accessed.

The domain classes use properties to represent their data.

---

### Interfaces

An interface defines a contract that a class can implement.

For this task, the following interface was created:

```csharp
public interface IPrint
{
    void PrintDetails();
}
````

[View IPrint.cs](./task4/IPrint.cs)

The interface is implemented by the domain classes that provide printing behavior.

---

### Polymorphism

Polymorphism was demonstrated by creating a method that accepts the `IPrint` interface:

```csharp
void Print(IPrint item)
{
    item.PrintDetails();
}
```

The same method can then work with different objects that implement `IPrint`:

```csharp
Print(book);
Print(member);
```

This allows the method to work with the interface type instead of depending on one specific class.

---

# 🧩 Domain Model

## Library Management System

The practical domain represents a simple library system containing books, authors, and members.

### Main Classes

#### Book

Represents a book in the library.

The object is populated with:

* `BookId`
* `Title`
* `Pages`
* `Author`
* `Member`

Example:

```csharp
Book book = new Book();

book.BookId = 10;
book.Title = "C#";
book.Pages = 1000;
book.Author = author;
book.Member = member;
```

[View Book.cs](./Models/Book.cs)

---

#### Author

Represents the author of a book.

The practical code uses:

* `AuthorId`
* `Name`

Example:

```csharp
Author author = new Author();

author.AuthorId = 1;
author.Name = "yara";
```

[View Author.cs](./Models/Author.cs)

---

#### Member

Represents a library member.

The practical code uses:

* `MemberId`
* `Name`
* `Email`

Example:

```csharp
Member member = new Member();

member.MemberId = 100;
member.Name = "MAYAR";
member.Email = "mayar@gmail.com";
```

[View Member.cs](./Models/Member.cs)

---

# 🧪 Hands-On Lab

The Day 3 lab focused on modeling a small domain and applying OOP concepts.

---

## Task 1 — Domain Classes and Object Relationships

The first task created objects from the domain classes:

```csharp
Author author = new Author();
Member member = new Member();
Book book = new Book();
```

The objects were populated and connected together:

```csharp
book.Author = author;
book.Member = member;
```

The program then printed information about the book:

```text
Book Title
Book Pages
Book Author
Book Member
```

This demonstrated creating objects, setting their properties, and accessing related objects.

---

## Task 2 — Records as Data Transfer Objects

The second task introduced record types for request data.

Three records were created:

### AddBookRequest

```csharp
public record AddBookRequest(
    string Title,
    int Pages,
    int AuthorId
);
```

[View AddBookRequest.cs](./Records/AddBookRequest.cs)

### RegisterMemberRequest

```csharp
public record RegisterMemberRequest(
    string Name,
    string Email,
    int MemberId
);
```

[View RegisterMemberRequest.cs](./Records/RegisterMemberRequest.cs)

### BorrowBookRequest

```csharp
public record BorrowBookRequest(
    int MemberId,
    int BookId
);
```

[View BorrowBookRequest.cs](./Records/BorrowBookRequest.cs)

The records were then instantiated and their values were displayed.

For example:

```csharp
AddBookRequest addBookRequest =
    new AddBookRequest("C# Programming", 1000, 1);
```

These records represent data that can be transferred between parts of an application.

---

## Task 3 — Constructor Usage

The third task created a `Bookk` object using a constructor:

```csharp
Bookk bookk = new Bookk("Clean Code", 464);
```

The object's properties were then displayed:

```csharp
Console.WriteLine(bookk.Title);
Console.WriteLine(bookk.Pages);
```

[View Bookk.cs](./Models/Bookk.cs)

This task provided practical experience with creating objects using constructors.

---

## Task 4 — Interfaces and Polymorphism

The fourth task created the `IPrint` interface:

```csharp
public interface IPrint
{
    void PrintDetails();
}
```

The program defined a method that accepts any object implementing `IPrint`:

```csharp
void Print(IPrint item)
{
    item.PrintDetails();
}
```

The objects were modified and then passed to the same method:

```csharp
book.Title = "C# Programming";
member.Name = "Yara";

Print(book);
Print(member);
```

This demonstrates polymorphism because the `Print` method works with the interface type rather than a specific concrete class.

---

# 💻 Implementation

All four tasks were implemented in the Day 3 console application.

### Program.cs

The main program contains the implementation for:

1. Creating and connecting library domain objects.
2. Creating and using record-based request types.
3. Creating a `Bookk` object through a constructor.
4. Using `IPrint` and demonstrating polymorphism.

[View Program.cs](./Program.cs)

---

## 🛠 Technologies Used

| Technology | Purpose                               |
| ---------- | ------------------------------------- |
| C#         | Programming language                  |
| .NET SDK   | Application development and execution |
| .NET CLI   | Building and running the project      |
| VS Code    | Development environment               |
| Git        | Version control                       |
| GitHub     | Repository management                 |

---

## 📁 Project Structure

```text
Day3/
│
├── Models/
│   ├── Author.cs
│   ├── Book.cs
│   ├── Bookk.cs
│   └── Member.cs
│
├── Records/
│   ├── AddBookRequest.cs
│   ├── BorrowBookRequest.cs
│   └── RegisterMemberRequest.cs
│
├── task4/
│   └── IPrint.cs
│
├── Program.cs
├── Day3.csproj
└── README.md
```

### Main Files

| File / Folder     | Purpose                                     |
| ----------------- | ------------------------------------------- |
| `Program.cs`      | Contains the Day 3 tasks and demonstrations |
| `Models/`         | Contains the library domain classes         |
| `Records/`        | Contains record types used as request data  |
| `task4/IPrint.cs` | Contains the `IPrint` interface             |
| `Day3.csproj`     | Project configuration                       |
| `README.md`       | Day 3 documentation                         |

---

# ▶️ Build and Run

Open a terminal inside the Day 3 project directory.

### Build the project

```bash
dotnet build
```

### Run the application

```bash
dotnet run
```

The application executes the four implemented tasks and displays their results in the console.

---

# 🎯 Learning Outcomes

By completing Day 3, I practiced:

* Creating and using C# classes.
* Creating objects from classes.
* Working with object properties.
* Connecting related objects.
* Understanding the purpose of records.
* Creating record-based data transfer objects.
* Using constructors to initialize objects.
* Understanding interfaces.
* Implementing an interface in domain classes.
* Passing objects through an interface type.
* Demonstrating polymorphism.
* Organizing classes, records, and interfaces into separate files.

---

## 🔗 Quick Links

| Resource               | Link                                                           |
| ---------------------- | -------------------------------------------------------------- |
| Program Code           | [Program.cs](./Program.cs)                                     |
| Author Model           | [Author.cs](./Models/Author.cs)                                |
| Book Model             | [Book.cs](./Models/Book.cs)                                    |
| Bookk Model            | [Bookk.cs](./Models/Bookk.cs)                                  |
| Member Model           | [Member.cs](./Models/Member.cs)                                |
| Add Book Record        | [AddBookRequest.cs](./Records/AddBookRequest.cs)               |
| Register Member Record | [RegisterMemberRequest.cs](./Records/RegisterMemberRequest.cs) |
| Borrow Book Record     | [BorrowBookRequest.cs](./Records/BorrowBookRequest.cs)         |
| IPrint Interface       | [IPrint.cs](./task4/IPrint.cs)                                 |
| Day 2                  | [← Day 2](../Day2/README.md)                                   |
| Week 1                 | [← Back to Week 1](../README.md)                               |
| Repository             | [← Back to Repository](../../README.md)                        |

---

## 📌 Day Information

| Item         | Details                     |
| ------------ | --------------------------- |
| Week         | 1                           |
| Day          | 3                           |
| Main Topic   | C# Fundamentals II          |
| Focus        | Object-Oriented Programming |
| Domain       | Library Management System   |
| Project Type | C# Console Application      |


