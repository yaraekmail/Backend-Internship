# Day 3 — Entity Framework Core Setup & Code-First Migrations

## Overview

Today I worked on setting up **Entity Framework Core** with **SQL Server** and connecting it to the Training Management API.

The implementation was based on the normalized database design created in Day 2. Instead of using the example `Customer` and `Order` entities from the lesson, I implemented the entities for my own **Training Management** domain.

The main goal was to configure EF Core, define the database entities, create the `DbContext`, configure the SQL Server connection, and generate the first code-first migration.

---

## Learning Objectives

* Set up Entity Framework Core with SQL Server.
* Define entity classes based on the Day 2 database schema.
* Create and configure a `DbContext`.
* Register the `DbContext` with ASP.NET Core Dependency Injection.
* Configure the SQL Server connection string.
* Create a Code-First migration.
* Inspect the generated migration and database schema.
* Understand how EF Core keeps the database schema synchronized with the application code.

---

## Project Structure

The Day 3 project is located inside:

`Week3/Day3/Day3_TrainingManagementApi`

Main structure:

```text
Day3
├── Day3week3documntation.docx
│
└── Day3_TrainingManagementApi
    ├── Program.cs
    ├── Day3_TrainingManagementApi.csproj
    │
    ├── Data
    │   └── TrainingManagementDbContext.cs
    │
    ├── Migrations
    │   ├── 20260811192605_InitialCreate.cs
    │   ├── 20260811192605_InitialCreate.Designer.cs
    │   └── TrainingManagementDbContextModelSnapshot.cs
    │
    └── Models
        ├── Company.cs
        ├── Course.cs
        ├── CourseSkill.cs
        ├── Instructor.cs
        ├── IslamicGoal.cs
        ├── Participant.cs
        ├── Semester.cs
        ├── Skill.cs
        ├── Task.cs
        ├── Trainer.cs
        ├── Training.cs
        ├── TrainingDay.cs
        ├── TrainingParticipant.cs
        ├── TrainingSkill.cs
        └── University.cs
```

---

## 1. Entity Framework Core Setup

Entity Framework Core was configured to work with **SQL Server**.

The project uses the EF Core SQL Server provider and the EF Core tools required for migrations.

The project file can be viewed here:

[Day3_TrainingManagementApi.csproj](./Day3_TrainingManagementApi/Day3_TrainingManagementApi.csproj)

---

## 2. Entity Models

I created entity classes representing the tables from the normalized Training Management database design.

The project contains **15 entities**:

* Company
* Course
* CourseSkill
* Instructor
* IslamicGoal
* Participant
* Semester
* Skill
* Task
* Trainer
* Training
* TrainingDay
* TrainingParticipant
* TrainingSkill
* University

The entity classes are located in:

[Models](./Day3_TrainingManagementApi/Models/)

Some of the models also represent relationship tables, such as:

* `CourseSkill`
* `TrainingParticipant`
* `TrainingSkill`

These allow the database to represent many-to-many relationships correctly.

---

## 3. DbContext

A custom `TrainingManagementDbContext` was created to serve as the main EF Core context for the application.

It is responsible for exposing the entities as `DbSet` properties and allowing EF Core to track changes and communicate with SQL Server.

[TrainingManagementDbContext.cs](./Day3_TrainingManagementApi/Data/TrainingManagementDbContext.cs)

---

## 4. Dependency Injection Configuration

The `TrainingManagementDbContext` was registered in the ASP.NET Core Dependency Injection container.

The configuration was added to `Program.cs` using `AddDbContext` and `UseSqlServer`.

[Program.cs](./Day3_TrainingManagementApi/Program.cs)

The application obtains the database connection through the configured `DefaultConnection` connection string.

This allows the database configuration to remain separate from the application code.

---

## 5. Connection String

The application uses a SQL Server connection string named:

`DefaultConnection`

The connection is retrieved through:

```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

The connection string is then passed to EF Core through `UseSqlServer()`.

> **Security note:** Real database passwords or credentials should never be committed to a public GitHub repository. Local development secrets should be kept outside the committed source code.

---

## 6. Code-First Migration

After defining the entities and configuring the `DbContext`, I created the initial EF Core migration.

The migration was generated using:

```bash
dotnet ef migrations add InitialCreate
```

The generated migration is available here:

[InitialCreate Migration](./Day3_TrainingManagementApi/Migrations/20260811192605_InitialCreate.cs)

EF Core also generated the migration designer file:

[InitialCreate.Designer.cs](./Day3_TrainingManagementApi/Migrations/20260811192605_InitialCreate.Designer.cs)

And the model snapshot:

[TrainingManagementDbContextModelSnapshot.cs](./Day3_TrainingManagementApi/Migrations/TrainingManagementDbContextModelSnapshot.cs)

---

## 7. Generated Database Structure

The migration reflects the Training Management database design created during Day 2.

The generated schema contains tables for:

* Companies
* Instructors
* IslamicGoals
* Participants
* Semesters
* Skills
* Trainers
* Universities
* Trainings
* Courses
* TrainingDays
* TrainingParticipants
* TrainingSkills
* CourseSkills
* Tasks

The migration also represents the relationships and foreign keys defined between the entities.

---

## 8. Program Configuration

The main application configuration is located in:

[Program.cs](./Day3_TrainingManagementApi/Program.cs)

The important EF Core configuration follows this structure:

```csharp
builder.Services.AddDbContext<TrainingManagementDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

This registers the `TrainingManagementDbContext` with Dependency Injection and configures it to use SQL Server.

---

## 9. Migration Workflow

The workflow followed during this task was:

```text
Entity Models
     ↓
TrainingManagementDbContext
     ↓
Register DbContext in Program.cs
     ↓
Configure SQL Server Connection
     ↓
dotnet ef migrations add InitialCreate
     ↓
Generated Migration
     ↓
Database Schema
```

This approach follows the **Code-First** workflow, where the C# entity model is used as the source of truth for generating database schema changes.

---

## 10. Commands Used

### Create the migration

```bash
dotnet ef migrations add InitialCreate
```

### Apply migrations to the database

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

---

## 11. What I Learned

During Day 3, I learned how Entity Framework Core connects an ASP.NET Core application to a SQL Server database.

I practiced:

* Creating EF Core entity models.
* Creating a custom `DbContext`.
* Using `DbSet<T>` to represent database tables.
* Registering the `DbContext` with Dependency Injection.
* Configuring SQL Server through a connection string.
* Creating Code-First migrations.
* Understanding the generated migration files.
* Keeping database schema changes version-controlled with the application code.
* Understanding the importance of protecting database credentials.

---

## 12. Documentation

The complete documentation for Day 3 is available here:

[Day 3 Documentation](./Day3week3documntation.docx)

---

## Related Days

* [Week 3](../README.md)
* [Day 1 — REST API Design & Resource Modeling](../Day1/)
* [Day 2 — SQL Server Schema Design & Normalization](../Day2/)
* [Day 3 — EF Core & Code-First Migrations](./)

---

## Technologies Used

* C#
* .NET 10
* ASP.NET Core
* Entity Framework Core
* SQL Server
* EF Core Migrations
* VS Code
* Git & GitHub
