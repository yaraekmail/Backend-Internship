# 📅 Day 4 – C# Collections, LINQ, Async/Await & Exception Handling

## 📖 Overview

In Day 4, I focused on advanced C# collections, learned how to query data efficiently using LINQ, explored asynchronous programming with `async` and `await`, and practiced handling runtime exceptions.

---

# 📚 Topics Covered

## Collections

- Hashtable
- Stack
- Queue
- LinkedList
- BitArray

### Collection Operations
- Creating collections
- Adding elements
- Removing elements
- Updating elements
- Searching for elements
- Iterating through collections
- Collection-specific methods and properties

---

## LINQ

- Introduction to LINQ
- Query Syntax
- Method Syntax
- `Where()`
- `Select()`
- `Sum()`
- LINQ Query Operators

---

## Asynchronous Programming

- Task
- async
- await
- Task.Delay()
- Returning values from asynchronous methods

---

## Exception Handling

- try
- catch
- finally
- throw
- Multiple Catch Blocks
- Custom Exceptions

---

# 📝 Hands-On Lab

### ✅ Task 1
Created a `List<Book>` containing eight objects with different property values.

### ✅ Task 2
Applied LINQ queries:
- Filter books using `Where()`.
- Project book titles using `Select()`.
- Calculate the total number of pages using `Sum()`.

### ✅ Task 3
Created an asynchronous method that:
- Simulates an I/O delay using `Task.Delay()`.
- Returns the total number of books.
- Retrieves the result using `await`.

### ✅ Task 4
Implemented exception handling by:
- Reading user input.
- Converting it to an integer using `int.Parse()`.
- Handling `FormatException`.
- Handling unexpected exceptions.

---

# 🎯 Learning Outcomes

After completing Day 4, I can:

- Work with different C# collection types and understand when to use each one.
- Perform common collection operations efficiently.
- Query collections using LINQ.
- Use filtering, projection, and aggregation operators.
- Write asynchronous methods using `async` and `await`.
- Handle runtime errors using structured exception handling.

---

# 🛠 Technologies

- C#
- .NET
- Collections
- LINQ
- Async/Await
- Exception Handling
- Visual Studio

---

# 📂 Project Structure

```
Day4/
│── Program.cs
│── Book.cs
│── Author.cs
│── Member.cs
│── README.md
```

---

# 👩‍💻 Author

**Yara Kmail**