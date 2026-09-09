# Week 6 — Day 2

## Building the Full EF Core Model & Migrations

### Hands-On Lab

#### 1. Implement Entity Classes

**Status: Done ✅**

The required entity classes were already present in the project and were reviewed to confirm that they match the Day 1 ERD and include the required navigation properties.

**Implementation:**

* [Patient.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/Patient.cs)
* [VitalSign.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/VitalSign.cs)
* [Medication.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/Medication.cs)
* [Appointment.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/Appointment.cs)
* [MedicalCondition.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/MedicalCondition.cs)
* [Allergy.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Entities/Allergy.cs)

---

#### 2. Configure Relationships Using Fluent API

**Status: Done ✅**

The existing `DbContext` was reviewed and confirmed to contain explicit one-to-many relationships between `Patient` and the related entities.

`DeleteBehavior.Cascade` was explicitly configured for these relationships.

**Implementation:**

* [CardiacPatientMonitoringDbContext.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)

---

#### 3. Add Seed Data Using `HasData`

**Status: Done ✅**

`HasData` was added to the `Patient` entity with one test patient to satisfy the Day 2 requirement.

**Implementation:**

* [CardiacPatientMonitoringDbContext.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)

---

#### 4. Generate and Review the Migration

**Status: Done ✅**

The initial `InitialCreate` migration already existed in the project.

For the Day 2 `HasData` change, the following migration was generated and reviewed before applying it:

`20260908190217_AddHasData`

The migration was confirmed to contain the expected `InsertData` operation for the test patient.

**Implementation:**

* [20260908190217_AddHasData.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Migrations/20260908190217_AddHasData.cs)

---

#### 5. Apply the Migration and Confirm the Database Schema

**Status: Done ✅**

The `AddHasData` migration was successfully applied using `dotnet ef database update`.

The migration history was verified and showed:

* `20260826203005_InitialCreate`
* `20260908190217_AddHasData`

The database was then opened in SQL Server Management Studio (SSMS), where the required tables from the Day 1 ERD were confirmed:

* `Patients`
* `VitalSigns`
* `Medications`
* `Appointments`
* `MedicalConditions`
* `Allergies`

**Database:**
`CardiacPatientMonitoringDb`

---


## Tools Used

* Entity Framework Core
* SQL Server
* SQL Server Management Studio (SSMS)
* .NET CLI
