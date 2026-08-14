# Week 3 — Backend Development & API Integration

## Overview

Week 3 focused on moving from backend concepts and database design into practical **REST API development, SQL Server integration, Entity Framework Core, migrations, and API testing with Postman**.

The week followed a continuous project workflow: designing the API, designing and normalizing the database, implementing the database layer, building CRUD endpoints, and finally testing and documenting the API.

---

## Week 3 Structure

| Day                       | Topic                                    | Summary                                                                                                                                                                                                                    |
| ------------------------- | ---------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Day 1](./Day1/README.md) | REST API Design & Resource Modeling      | Designed a REST resource map for the **Student Personal Development & Task Management** domain. Defined resource names, CRUD endpoints, HTTP methods, status codes, nested resources, and API versioning using `/api/v1/`. |
| [Day 2](./Day2/README.md) | SQL Server Schema Design & Normalization | Designed a normalized relational database schema based on the API resources. Applied **1NF, 2NF, and 3NF**, defined primary and foreign keys, modeled relationships, and prepared the ERD.                                 |
| [Day 3](./Day3/README.md) | EF Core & Code-First Migrations          | Implemented the Day 2 schema using **Entity Framework Core and SQL Server**. Created the project entities, `TrainingManagementDbContext`, configured the connection, and generated the `InitialCreate` migration.          |
| [Day 4](./day4/README.md) | CRUD API Development                     | Built a **Participants API** using ASP.NET Core and EF Core. Implemented GET, POST, PUT, and DELETE endpoints with appropriate success and error status codes.                                                             |
| [Day 5](./Day5/README.md) | API Testing & Documentation with Postman | Tested the Participants API using an organized **Postman Collection** with success and error paths. Added status-code test scripts, configured `{{baseUrl}}`, exported the collection, and documented the results.         |

---

## 🔗 Day Navigation

### [Day 1 — REST API Design & Resource Modeling →](./Day1/README.md)

Designed the REST API around resources and established consistent endpoint naming, HTTP methods, status codes, nested resources, and API versioning.

### [Day 2 — SQL Server Schema Design & Normalization →](./Day2/README.md)

Converted the API resources into a normalized relational database design and modeled its tables, keys, and relationships.

### [Day 3 — EF Core & Code-First Migrations →](./Day3/README.md)

Connected the normalized schema to an ASP.NET Core application using EF Core, SQL Server, `DbContext`, and Code-First migrations.

### [Day 4 — CRUD API Development →](./day4/README.md)

Implemented the Participants CRUD API and connected the controller to the database through Entity Framework Core.

### [Day 5 — Postman API Testing →](./Day5/README.md)

Tested the API systematically using Postman, covering success and error scenarios, automated status checks, environments, variables, and collection export.

---

## 🔄 Week 3 Workflow

```text
REST API Design
      ↓
Database Schema & Normalization
      ↓
Entity Framework Core
      ↓
CRUD API
      ↓
Postman Testing & Documentation
```

---

## 🧠 Week 3 Skills

* REST API Design
* Resource Modeling
* HTTP Methods & Status Codes
* SQL Server
* Database Normalization
* Primary & Foreign Keys
* Entity Framework Core
* Code-First Migrations
* ASP.NET Core Web API
* CRUD Operations
* Postman
* API Testing
* Environments & Variables
* API Documentation

---

## 🎯 Week 3 Outcome

By the end of Week 3, I moved from **API and database design** to implementing and testing a working backend API.

The week connected the full development flow: **design → database → EF Core → API → testing → documentation**.

### 🔗 Quick Links

[Day 1](./Day1/README.md) ·
[Day 2](./Day2/README.md) ·
[Day 3](./Day3/README.md) ·
[Day 4](./day4/README.md) ·
[Day 5](./Day5/README.md)
