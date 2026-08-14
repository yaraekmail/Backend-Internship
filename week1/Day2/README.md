
# Week 1 - Day 2 | C# Fundamentals I

> Practicing C# types, variables, value vs. reference behavior, control flow, switch expressions, and nullable reference types.

---

## 📚 Table of Contents

- [Overview](#-overview)
- [Topics Covered](#-topics-covered)
- [Hands-On Lab](#-hands-on-lab)
- [Implementation](#-implementation)
- [Technologies Used](#-technologies-used)
- [Project Structure](#-project-structure)
- [Build and Run](#-build-and-run)
- [Learning Outcomes](#-learning-outcomes)
- [Quick Links](#-quick-links)

---

## 📌 Overview

Day 2 focused on the fundamentals of C# types, variables, and control flow.

The practical work was completed through four tasks in a C# Console Application. The tasks covered:

- Identifying value types and reference types.
- Using `GetType()` to inspect variable types.
- Demonstrating value-type and reference-type copy behavior.
- Using methods and local variables.
- Creating a grade classifier using a switch expression.
- Reading user input safely when the value may be `null`.

---

## 📖 Topics Covered

### Value Types vs. Reference Types

Value types store their data directly and are copied by value.

Examples practiced in the task include:

```csharp
int
bool
double
````

Reference types hold a reference to an object.

Examples practiced include:

```csharp
string
int[]
string[]
```

The difference was demonstrated practically by copying variables and observing what happens when one of the copies is modified.

---

### Variables, Type Inference, and Naming

The lesson covered:

* Variables
* Explicit types
* Type inference using `var`
* Clear and consistent naming conventions

For example:

```csharp
var count = 5;
```

The practical code mainly used explicit types such as `int`, `bool`, `double`, and `string`.

---

### Control Flow

The lesson covered several ways to control program flow:

* `if`
* `switch`
* Switch expressions
* `for`
* `foreach`
* `while`

The practical task specifically implemented a **switch expression** for classifying grades.

---

### Nullable Reference Types

Nullable reference types help identify values that may contain `null`.

The practical code used:

```csharp
string? names = Console.ReadLine();
```

The value was then checked before being printed:

```csharp
if (names != null)
    Console.WriteLine(names);
else
    Console.WriteLine("no name");
```

---

# 🧪 Hands-On Lab

The Day 2 lab contained four implemented tasks.

---

## Task 1 — Value Types and Reference Types

The first task created at least three value-type variables and three reference-type variables.

```csharp
int x = 5;
bool y = true;
double z = 550.2;

string s = "yara";
int[] num = { 1, 2, 3, 4 };
string[] name = { "yara", "eyad", "kmail" };
```

Each variable's type was displayed using `GetType()`.

Example:

```csharp
Console.WriteLine($"x : {x.GetType()}");
Console.WriteLine($"s : {s.GetType()}");
```

The output was organized into:

```text
=== Value Types ===

=== Reference Types ===
```

---

## Task 2 — Value vs. Reference Copy Behavior

The second task demonstrated the difference between copying a value type and copying a reference type.

### Value Type

An `int` was copied to another variable:

```csharp
int x = 33;
int y = x;
```

After changing `y`, the original `x` remained unchanged.

This demonstrates that the value itself is copied.

### Reference Type

An array was assigned to another variable:

```csharp
int[] yara = { 1, 2, 3, 4 };
int[] kmail = yara;
```

After changing the first element through `kmail`:

```csharp
kmail[0] = 1289;
```

the value observed through `yara` also changed.

This demonstrates that both variables refer to the same array object.

The program printed the values both before and after the modification.

---

## Task 3 — Grade Classifier

The third task implemented a grade classifier using a C# switch expression.

```csharp
string grade = score switch
{
    >= 90 => "excelent",
    >= 80 => "very good",
    >= 50 => "pass",
    <= 49 => "fail"
};
```

The method was tested with several scores:

```csharp
grade(99);
grade(88);
grade(70);
grade(30);
```

This demonstrated how a switch expression can map different value ranges to different results.

---

## Task 4 — Nullable User Input

The fourth task read input from the console:

```csharp
string? names = Console.ReadLine();
```

Since `Console.ReadLine()` may return `null`, the program checks the value before using it:

```csharp
if (names != null)
    Console.WriteLine(names);
else
    Console.WriteLine("no name");
```

This demonstrates safe handling of possibly-null reference values.

---

# 💻 Implementation

All four tasks were implemented in the Day 2 console application.

### Program.cs

The program contains the implementations for:

1. Value and reference type identification.
2. Value vs. reference copy behavior.
3. Grade classification using a switch expression.
4. Safe handling of nullable user input.

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
Day2/
│
├── Program.cs
├── Day2.csproj
├── README.md
└── .cph/
    └── .Program.cs_a4959e3d3cbb7a290048eb97c6965c75.prob
```

### Main Files

| File          | Description                 |
| ------------- | --------------------------- |
| `Program.cs`  | Contains the Day 2 C# tasks |
| `Day2.csproj` | Project configuration       |
| `README.md`   | Day 2 documentation         |

---

# ▶️ Build and Run

Open a terminal inside the Day 2 project directory.

### Build the project

```bash
dotnet build
```

### Run the application

```bash
dotnet run
```

The application runs the implemented tasks and displays their results in the console.

---

# 🎯 Learning Outcomes

By completing Day 2, I practiced:

* Distinguishing value types from reference types.
* Using `GetType()` to inspect variable types.
* Understanding how value types are copied.
* Understanding how reference types behave when copied.
* Declaring and using variables.
* Understanding type inference with `var`.
* Using `if` conditions.
* Using switch expressions.
* Understanding the purpose of loops.
* Working with nullable reference types.
* Safely handling possibly-null user input.
* Organizing multiple C# exercises in one console application.

---

## 🔗 Quick Links

| Resource     | Link                                    |
| ------------ | --------------------------------------- |
| Program Code | [Program.cs](./Program.cs)              |
| Project File | [Day2.csproj](./Day2.csproj)            |
| Day 1        | [← Day 1](../Day1/README.md)            |
| Week 1       | [← Back to Week 1](../README.md)        |
| Repository   | [← Back to Repository](../../README.md) |

---

## 📌 Day Information

| Item         | Details                         |
| ------------ | ------------------------------- |
| Week         | 1                               |
| Day          | 2                               |
| Main Topic   | C# Fundamentals I               |
| Main Focus   | Types, Variables & Control Flow |
| Project Type | C# Console Application          |

