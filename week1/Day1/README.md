
# Week 1 - Day 1 | Program Orientation & .NET Environment Setup

> The first day of the Backend Internship focused on understanding the program structure, setting up the .NET development environment, learning the .NET CLI, and creating the first C# Console Application.

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

Day 1 introduced the Backend Internship program and established the development environment that will be used throughout the training.

The practical work focused on:

- Understanding the overall internship structure and phases.
- Setting up and verifying the .NET development environment.
- Learning the basic `dotnet` CLI commands.
- Creating and running a C# Console Application.
- Practicing basic C# console output.
- Using Git and GitHub as part of the development workflow.

The day ended with a working C# console application that displays personal information and the current date and time.

---

## 📖 Topics Covered

### 1. Program Orientation

The first part of the day introduced the internship structure, including:

- Program phases
- Weekly progression
- Deliverables
- Evaluation
- Mentor check-ins
- GitHub and project tracking
- The overall progression toward the later stages of the program

The orientation provided an overview of how the different weeks build on each other.

---

### 2. .NET SDK

The .NET SDK provides the tools required to develop and run .NET applications.

The SDK includes:

- .NET runtime
- Compiler
- `dotnet` CLI

The environment was verified using:

```bash
dotnet --version
````

The installed SDK can also be inspected using:

```bash
dotnet --list-sdks
```

---

### 3. .NET CLI

The `dotnet` CLI is used throughout the project for creating, building, and running .NET applications.

The main commands introduced during Day 1 were:

```bash
dotnet new console
dotnet build
dotnet run
```

#### `dotnet new console`

Creates a new C# Console Application.

#### `dotnet build`

Builds the project and checks whether the code compiles successfully.

#### `dotnet run`

Builds and runs the application.

---

### 4. IDE Setup

The development environment can be configured using an IDE such as:

* Visual Studio Code with C# Dev Kit
* Visual Studio

The goal was to have a working development environment with code completion and debugging support.

---

## 🧪 Hands-On Lab

The Day 1 practical lab was focused on environment setup and creating the first console application.

The lab tasks included:

| Task | Description                                                             |
| ---- | ----------------------------------------------------------------------- |
| 1    | Install and verify the .NET SDK                                         |
| 2    | Configure the chosen IDE                                                |
| 3    | Create and run a new console application                                |
| 4    | Modify the program to display personal information and the current date |
| 5    | Create and use a GitHub repository for the internship                   |

---

## 💻 Implementation

The Day 1 application is a simple C# Console Application.

### Program.cs

The program prints:

* My name
* University
* Internship information
* Current date and time

```csharp
Console.WriteLine("Hello, I'm Yara Kmail");
Console.WriteLine("Arab American University");
Console.WriteLine("Backend Internship - Day 1");
Console.WriteLine(DateTime.Now);
```

The `DateTime.Now` property is used to display the current date and time when the application runs.

[View Program.cs](./Program.cs)

---

## 🛠 Technologies Used

| Technology               | Purpose                                     |
| ------------------------ | ------------------------------------------- |
| C#                       | Programming language                        |
| .NET                     | Application development platform            |
| .NET CLI                 | Creating, building, and running the project |
| Visual Studio Code / IDE | Development environment                     |
| Git                      | Version control                             |
| GitHub                   | Repository and project management           |

---

## 📁 Project Structure

```text
Day1/
│
├── Program.cs
├── Day1.csproj
└── README.md
```

### Main Files

| File          | Description                              |
| ------------- | ---------------------------------------- |
| `Program.cs`  | Contains the C# console application code |
| `Day1.csproj` | Contains the project configuration       |
| `README.md`   | Documentation for Day 1                  |

---

## ▶️ Build and Run

Open a terminal inside the Day 1 project directory.

### Build the project

```bash
dotnet build
```

### Run the application

```bash
dotnet run
```

The application then prints the configured information and the current date and time.

---

## 🎯 Learning Outcomes

By completing Day 1, I practiced:

* Understanding the structure of the Backend Internship.
* Setting up a .NET development environment.
* Verifying the installed .NET SDK.
* Using the .NET CLI.
* Creating a C# Console Application.
* Building a .NET project.
* Running a .NET project.
* Writing basic C# console output.
* Using `DateTime.Now`.
* Working with Git and GitHub.

---

## 🔗 Quick Links

| Resource     | Link                                    |
| ------------ | --------------------------------------- |
| Program Code | [Program.cs](./Program.cs)              |
| Project File | [Day1.csproj](./Day1.csproj)            |
| Week 1       | [← Back to Week 1](../README.md)        |
| Repository   | [← Back to Repository](../../README.md) |

---

## 📌 Day Information

| Item         | Details                                    |
| ------------ | ------------------------------------------ |
| Week         | 1                                          |
| Day          | 1                                          |
| Main Project | C# Console Application                     |
| Main Focus   | .NET Environment Setup & First Console App |


