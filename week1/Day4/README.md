
# Day 4 - C# Fundamentals III: Collections, LINQ, Async/Await & Exception Handling

## Overview

This project is part of Week 1 of the Backend Internship.

During Day 4, I practiced working with C# collections, LINQ queries, asynchronous programming using `async` and `await`, and exception handling.

The practical work was based on the Library Management domain created during Day 3.

---

## Table of Contents

- [Overview](#overview)
- [Topics Covered](#topics-covered)
  - [Collections](#collections)
  - [LINQ](#linq)
  - [Async/Await](#asyncawait)
  - [Exception Handling](#exception-handling)
- [Hands-On Lab](#hands-on-lab)
  - [Task 1 - Create a List](#task-1---create-a-list)
  - [Task 2 - LINQ Queries](#task-2---linq-queries)
  - [Task 3 - Async/Await](#task-3---asyncawait)
  - [Task 4 - Exception Handling](#task-4---exception-handling)
- [Project Structure](#project-structure)
- [Technologies](#technologies)
- [Learning Outcomes](#learning-outcomes)
- [Day 4 Tasks Summary](#day-4-tasks-summary)
- [Quick Links](#quick-links)
- [Project Information](#project-information)

---

## Topics Covered

### Collections

The day introduced different collection types and how to work with collections.

Topics included:

- Hashtable
- Stack
- Queue
- LinkedList
- BitArray
- Creating collections
- Adding elements
- Removing elements
- Updating elements
- Searching for elements
- Iterating through collections
- Collection-specific methods and properties

### LINQ

LINQ was used to query and transform the collection of books.

Topics practiced:

- LINQ
- Method Syntax
- `Where()`
- `Select()`
- `Sum()`

### Async/Await

Asynchronous programming was practiced using:

- `Task`
- `async`
- `await`
- `Task.Delay()`
- Returning values from asynchronous methods

### Exception Handling

Exception handling was practiced using:

- `try`
- `catch`
- `finally`
- `throw`
- Multiple Catch Blocks
- Custom Exceptions
- `FormatException`

---

## Hands-On Lab

### Task 1 - Create a List

A `List<Book>` was created using the `Book` model from Day 3.

The list contains eight books with different IDs, titles, and page counts.

```csharp
List<Book> books = new List<Book>()
{
    new Book { BookId = 1, Title = "C#", Pages = 500 },
    new Book { BookId = 2, Title = "Java", Pages = 450 },
    new Book { BookId = 3, Title = "Operating System", Pages = 700 },
    new Book { BookId = 4, Title = "Networking", Pages = 350 },
    new Book { BookId = 5, Title = "Digital Lab", Pages = 200 },
    new Book { BookId = 6, Title = "Microprocessor", Pages = 600 },
    new Book { BookId = 7, Title = "Organization", Pages = 420 },
    new Book { BookId = 8, Title = "Digital Design", Pages = 380 }
};
````

[Open Program.cs](./Program.cs)

---

### Task 2 - LINQ Queries

Three LINQ queries were implemented against the books list.

#### Filter

`Where()` was used to find books with more than 500 pages.

```csharp
var res1 = books.Where(n => n.Pages > 500);
```

#### Projection

`Select()` was used to retrieve the titles of the books.

```csharp
var res2 = books.Select(n => n.Title);
```

#### Aggregation

`Sum()` was used to calculate the total number of pages.

```csharp
var res3 = books.Sum(n => n.Pages);
```

The results were then printed to the console.

[Open Program.cs](./Program.cs)

---

### Task 3 - Async/Await

An asynchronous method named `GetBookCount()` was created.

The method uses `Task.Delay()` to simulate an I/O delay and returns the number of books in the list.

```csharp
async Task<int> GetBookCount()
{
    await Task.Delay(3000);
    return books.Count;
}
```

The method was called using `await`:

```csharp
int count = await GetBookCount();

Console.WriteLine($"Number of books: {count}");
```

[Open Program.cs](./Program.cs)

---

### Task 4 - Exception Handling

User input was read from the console and converted into an integer using `int.Parse()`.

The operation was placed inside a `try` block, with a specific `FormatException` catch for invalid numeric input.

```csharp
Console.WriteLine("Enter Book Id: ");
string input = Console.ReadLine();

try
{
    int id = int.Parse(input);
    Console.WriteLine($"Book Id: {id}");
}
catch (FormatException)
{
    Console.WriteLine("Please enter a valid number.");
}
catch (Exception)
{
    Console.WriteLine("Unexpected error.");
}
```

[Open Program.cs](./Program.cs)

---

## Project Structure

```text
Day4/
│
├── Models_day3/
│   ├── Author.cs
│   ├── Book.cs
│   ├── Bookk.cs
│   └── Member.cs
│
├── task4day3/
│   └── IPrint.cs
│
├── Day4.csproj
├── Program.cs
└── README.md
```

The `Models_day3` folder contains the domain models created during Day 3 and reused in Day 4.

The `task4day3` folder contains the `IPrint` interface from Day 3.

---

## Technologies

* C#
* .NET
* LINQ
* Async/Await
* Exception Handling
* Visual Studio
* Git
* GitHub

---

## Learning Outcomes

After completing Day 4, I practiced:

* Working with different C# collection types.
* Creating and working with a `List<Book>`.
* Querying collections using LINQ.
* Using `Where()` for filtering.
* Using `Select()` for projection.
* Using `Sum()` for aggregation.
* Writing asynchronous methods using `async` and `await`.
* Using `Task.Delay()` to simulate an asynchronous operation.
* Handling invalid user input using `try` and `catch`.
* Handling `FormatException`.

---

## Day 4 Tasks Summary

| Task   | Description                                            | Status    |
| ------ | ------------------------------------------------------ | --------- |
| Task 1 | Create a List with at least 8 Book objects             | Completed |
| Task 2 | Write filter, projection, and aggregation LINQ queries | Completed |
| Task 3 | Create and await an async method using Task.Delay      | Completed |
| Task 4 | Handle user input using try/catch and FormatException  | Completed |

---

## Quick Links

| File             | Link                                      |
| ---------------- | ----------------------------------------- |
| Main Program     | [Open Program.cs](./Program.cs)           |
| Book Model       | [Open Book.cs](./Models_day3/Book.cs)     |
| Author Model     | [Open Author.cs](./Models_day3/Author.cs) |
| Member Model     | [Open Member.cs](./Models_day3/Member.cs) |
| Bookk Model      | [Open Bookk.cs](./Models_day3/Bookk.cs)   |
| IPrint Interface | [Open IPrint.cs](./task4day3/IPrint.cs)   |
| Project File     | [Open Day4.csproj](./Day4.csproj)         |

---

## Project Information

| Item            | Details                                            |
| --------------- | -------------------------------------------------- |
| Week            | 1                                                  |
| Day             | 4                                                  |
| Main Domain     | Library Management                                 |
| Main Model Used | Book                                               |
| Framework       | .NET                                               |
| Language        | C#                                                 |
| Main Topics     | Collections, LINQ, Async/Await, Exception Handling |

```
