# Week 6 — Sprint 1: Core Routes & Database Schema

## Overview

Week 6 was the first sprint of Phase 3 for the Cardiac Patient Monitoring System.

The sprint focused on reviewing and strengthening the existing database foundation, implementing core patient read operations, and adding a medication order workflow with real business logic and transaction handling.

The main Sprint 1 work included:

* Sprint planning and backlog sizing.
* Reviewing and documenting the existing database schema.
* Reviewing EF Core entities and relationships.
* Applying seed data and EF Core migrations.
* Implementing paginated patient catalog operations.
* Adding filtering and sorting through query parameters.
* Projecting database results to response DTOs.
* Implementing medication order creation.
* Adding stock validation and stock decrement.
* Calculating medication prices and order totals on the server.
* Protecting the order workflow with a database transaction.
* Adding optimistic concurrency handling with `RowVersion`.
* Demonstrating the API through Swagger.
* Completing the Sprint Review and Sprint 1 retrospective.

---

## Sprint 1 Goal

Improve the existing Cardiac Patient Monitoring System by adding efficient API read operations while keeping the existing database and core API, then extend the system with a transactional medication order workflow.

---

# Week 6 Progress

## Day 1 — Sprint Planning & Project Database Design

Day 1 focused on Sprint 1 planning and reviewing the existing database foundation.

The existing Cardiac Patient Monitoring database schema was reviewed, the ERD was finalized, and the Sprint 1 backlog was divided into manageable tasks.

### Main Work

* Reviewed the existing database schema and EF Core relationships.
* Finalized and documented the database ERD.
* Identified the main domain entities:

  * Patient
  * VitalSign
  * Medication
  * Appointment
  * MedicalCondition
  * Allergy
  * IdentityUser
* Defined the main relationships between `Patient` and its related entities.
* Created a sized Sprint 1 backlog.

### Documentation

[Day 1 Documentation](Day1/README.md)

### Key Code

[CardiacPatientMonitoringDbContext.cs](Day2/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)

---

## Day 2 — EF Core Data Model & Migrations

Day 2 focused on reviewing the full EF Core model, configuring relationships through the Fluent API, adding seed data, and applying the database migration.

### Main Work

* Reviewed the existing entity classes against the Day 1 ERD.
* Confirmed navigation properties and relationships.
* Reviewed explicit Fluent API relationship configuration.
* Confirmed `DeleteBehavior.Cascade` for the configured relationships.
* Added seed data using `HasData`.
* Generated and reviewed the `AddHasData` migration.
* Applied the migration using the .NET CLI.
* Verified the migration history and database tables through SSMS.

### Migration

`20260908190217_AddHasData`

### Documentation

[Day 2 Documentation](Day2/README.md)

### Key Code

[CardiacPatientMonitoringDbContext.cs](Day2/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)

[AddHasData Migration](Day2/CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Migrations/20260908190217_AddHasData.cs)

---

## Day 3 — Core Routes I: Catalog & Read Operations

Day 3 focused on implementing efficient patient catalog and read operations.

### Main Work

* Added pagination using `page` and `pageSize`.
* Added filtering by gender.
* Added filtering by city.
* Added sorting by city and name.
* Returned `totalCount` with paginated results.
* Used `AsNoTracking()` for read-only queries.
* Projected database results to `PatientResponse` DTOs.
* Added validation for pagination parameters.
* Tested individual and combined pagination, filtering, and sorting scenarios through Swagger.

### Documentation

[Day 3 Documentation](Day3/README.md)

### Key Code

[PatientsController.cs](Day3/CardiacPatientMonitoring.Api/Controllers/PatientsController.cs)

---

## Day 4 — Core Routes II: Write Operations & Business Logic

Day 4 focused on implementing the medication order workflow with business rules and transactional database operations.

### Main Work

* Added medication catalog items.
* Added medication orders and order items.
* Added request and response DTOs.
* Implemented `IMedicationOrderService`.
* Implemented `MedicationOrderService`.
* Added the medication order controller.
* Validated patient existence.
* Validated requested quantities.
* Checked medication stock availability.
* Calculated prices server-side.
* Calculated line totals and the complete order total.
* Decreased medication stock after successful orders.
* Wrapped order creation and stock updates in a database transaction.
* Added optimistic concurrency handling using `RowVersion`.
* Added EF Core migrations for the new order workflow.
* Added medication catalog seed data.
* Verified successful orders, insufficient stock, and transaction rollback behavior.

### Migrations

* `20260909133530_AddMedicationOrders`
* `20260909141715_AddMedicationCatalogConcurrency`

### Documentation

[Day 4 Documentation](Day4/README.md)

### Key Code

[MedicationOrderService.cs](Day4/CardiacPatientMonitoring.Api/Services/MedicationOrderService.cs)

[MedicationOrdersController.cs](Day4/CardiacPatientMonitoring.Api/Controllers/MedicationOrdersController.cs)

---

## Day 5 — Sprint Review, API Demo & Retrospective

Day 5 focused on demonstrating the completed Sprint 1 functionality, checking the sprint requirements, documenting remaining work, and completing the retrospective.

The API demonstration was performed through **Swagger**.

### Main Work

* Demonstrated the paginated patient catalog.
* Demonstrated filtering and sorting.
* Created a medication order end-to-end.
* Verified stock decrement before and after the order.
* Tested insufficient stock handling.
* Verified transaction rollback behavior from Day 4.
* Reviewed Sprint 1 acceptance criteria.
* Identified remaining work for Sprint 2.
* Completed the Sprint 1 retrospective.
* Defined one concrete action for Sprint 2.

### Sprint 1 Review Result

The main Sprint 1 API requirements were completed and demonstrated successfully.

Mentor code review and approval were not completed during Sprint 1, so this remains follow-up work for Sprint 2.

### Documentation

[Day 5 Documentation](Day5/README.md)

### Key Code

No new production code was added on Day 5. The day focused on API demonstration, sprint review, backlog updates, and retrospective.

---

# Sprint 1 Deliverables

| Deliverable                           | Status                      |
| ------------------------------------- | --------------------------- |
| Sprint 1 goal and sized backlog       | Completed                   |
| Documented database ERD               | Completed                   |
| EF Core data model                    | Completed                   |
| Fluent API relationship configuration | Completed                   |
| Seed data                             | Completed                   |
| Applied EF Core migrations            | Completed                   |
| Paginated catalog endpoint            | Completed                   |
| Filterable catalog endpoint           | Completed                   |
| Sortable catalog endpoint             | Completed                   |
| DTO projection                        | Completed                   |
| Medication order creation             | Completed                   |
| Stock validation                      | Completed                   |
| Server-side price calculation         | Completed                   |
| Stock decrement                       | Completed                   |
| Database transaction                  | Completed                   |
| Optimistic concurrency handling       | Completed                   |
| Sprint Review                         | Completed                   |
| Sprint Retrospective                  | Completed                   |
| Mentor code review and approval       | Carried forward to Sprint 2 |

---

# API Features Implemented

## Patients Catalog

The Patients catalog supports:

* Pagination
* Gender filtering
* City filtering
* Sorting
* DTO projection
* Total result count
* Read-only EF Core queries

Example route:

`GET /api/Patients`

Supported query parameters include:

* `page`
* `pageSize`
* `sort`
* `gender`
* `city`

---

## Medication Orders

The medication order workflow supports:

* Patient validation.
* Medication catalog validation.
* Quantity validation.
* Stock availability checks.
* Server-side price calculation.
* Line total calculation.
* Order total calculation.
* Stock decrement.
* Database transactions.
* Optimistic concurrency handling.

Example route:

`POST /api/MedicationOrders`

---

# Database

The project uses:

* SQL Server
* SQL Server LocalDB
* Entity Framework Core
* Code-first migrations

Database:

`CardiacPatientMonitoringDb`

The database contains the core patient monitoring entities as well as the medication order workflow introduced during Sprint 1.

---

# Migrations

The relevant migration history developed during Week 6 includes:

* `20260826203005_InitialCreate`
* `20260908190217_AddHasData`
* `20260909133530_AddMedicationOrders`
* `20260909141715_AddMedicationCatalogConcurrency`

---

# Testing & Verification

API functionality was demonstrated through Swagger using both successful and error scenarios.

The Sprint 1 demonstrations covered:

* Paginated patient results.
* Patient filtering by gender.
* Patient filtering by city.
* Patient sorting.
* Combined filtering, sorting, and pagination.
* Successful medication order creation.
* Stock decrement after a successful order.
* Insufficient stock rejection.
* Transaction rollback verification.

---

# Sprint 2 Backlog

The following items were carried forward to Sprint 2:

1. **Complete Mentor Code Review**

   * Review the Sprint 1 implementation with the mentor.
   * Address any feedback received during the review.

2. **Improve Automated Tests**

   * Add automated tests for the medication order business logic.
   * Cover successful order creation.
   * Cover insufficient stock scenarios.
   * Add coverage for transaction rollback behavior.

3. **Continue API Improvements**

   * Apply improvements identified during code review.
   * Continue validating the API through successful and error scenarios.

---

# Sprint 1 Retrospective

## What Went Well

* The Patients catalog features were implemented and tested successfully.
* Pagination, filtering, and sorting were demonstrated through Swagger.
* The medication order workflow included real business logic beyond basic CRUD.
* Stock validation and stock decrement were tested successfully.
* Transaction handling and rollback were verified.
* Both successful and error scenarios were tested during the Sprint 1 API demonstration.

## What Could Be Improved

* Mentor code review was not completed during Sprint 1.
* More automated tests are needed for the medication order business logic.
* Code review should be included earlier in the development process.

## Concrete Action for Sprint 2

**Write automated tests for the medication order service before adding the next major write operation.**

This will help detect business logic and transaction-related issues earlier and make the implementation easier to review.

---

# Technical Stack

| Category                    | Technology                   |
| --------------------------- | ---------------------------- |
| Language                    | C#                           |
| API Framework               | ASP.NET Core Web API         |
| ORM                         | Entity Framework Core        |
| Database                    | SQL Server / LocalDB         |
| API Documentation & Testing | Swagger                      |
| Authentication              | ASP.NET Core Identity + JWT  |
| Version Control             | Git & GitHub                 |
| Database Management         | SQL Server Management Studio |

---

# Final Status

**Sprint 1 — Completed**

The main Sprint 1 API requirements were implemented, tested, and demonstrated successfully.

The sprint established the database foundation, patient catalog operations, and medication order workflow required for the next phase of the project.

**Remaining follow-up:** Mentor code review and approval, together with any resulting feedback, will be addressed in Sprint 2.
