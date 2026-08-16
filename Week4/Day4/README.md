# Week 4 — Day 4: Input Validation with FluentValidation

## Overview

This project extends the **Training Management API** from Week 3 — Day 3.

The main focus of Day 4 is implementing **server-side input validation** using **FluentValidation**, integrating validators into the ASP.NET Core pipeline, and testing validation behavior using **Postman**.

---

## Learning Objectives

- Understand **DataAnnotations vs FluentValidation**.
- Create validators for Create and Update request models.
- Apply real business validation rules.
- Register validators using Dependency Injection.
- Return structured `400 Bad Request` validation responses.
- Test validation rules individually using Postman.

---

## Technologies

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- FluentValidation
- ASP.NET Core Identity
- JWT Bearer Authentication
- Postman

---

## Project Structure

```text
Day4/
├── Controllers/
│   ├── AuthController.cs
│   └── ParticipantsController.cs
├── Data/
│   └── TrainingManagementDbContext.cs
├── Models/
├── Migrations/
├── Validators/
│   ├── CreateParticipantValidator.cs
│   └── UpdateParticipantValidator.cs
├── postman/
│   └── Week 4 - Day 4 - Input Validation.postman_collection.json
├── Documentation/
│   └── Week_4Day4.docx
├── Program.cs
├── Day4.csproj
└── README.md