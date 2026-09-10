# Week 7 — Day 1

## Sprint 2 — Authentication & Role-Based Access

### Day 1: Sprint Planning & Identity Integration

### Sprint Goal

Start Sprint 2 by connecting the existing patient data with ASP.NET Core Identity, so the system can later identify the patient who owns an account and apply role-based and resource-based authorization.

---

## What We Did

### 1. Sprint Planning

We started Sprint 2 by defining the main goal and planning the work for the five training days.

The Sprint 2 roles were planned as:

* **Patient** — for patient accounts.
* **Admin** — for administrative access.

We also carried forward the testing improvement from Sprint 1.

---

### 2. Reviewed the Existing Identity Setup

The project already had ASP.NET Core Identity integrated into the `DbContext`.

The existing database migration history also already contained the Identity tables, so there was no need to create Identity tables again.

---

### 3. Connected Patient with Identity User

Before Day 1, the `Patient` entity contained the patient's medical and personal information, while `IdentityUser` handled login accounts.

The missing part was the connection between them.

We added:

* `UserId` to store the Identity user's ID.
* `User` as a navigation property.

The `UserId` is nullable because not every patient in the system needs to have a login account.

**Updated file:**

`CardiacPatientMonitoring.Api/Entities/Patient.cs`

---

### 4. Added the EF Core Relationship

A one-to-one relationship was added between `Patient` and `IdentityUser`.

The relationship uses `SetNull` when an Identity user is deleted.

This means deleting a login account does not delete the patient's medical data. Instead, the patient's `UserId` becomes `NULL`.

**Updated file:**

`CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs`

---

### 5. Created and Applied a Migration

After changing the model, we created:

`AddPatientIdentityLink`

The migration:

* Adds the nullable `UserId` column to `Patients`.
* Creates a unique index for `UserId`.
* Creates the foreign key between `Patients.UserId` and `AspNetUsers.Id`.
* Uses `SetNull` when the Identity user is deleted.

The migration was reviewed before applying it to the database.

**Migration file:**

`CardiacPatientMonitoring.Api/Migrations/20260910071042_AddPatientIdentityLink.cs`

The migration was successfully applied to the database.

---

## Result

At the end of Day 1, the project has this relationship:

```text
IdentityUser
     │
     │ UserId
     ↓
  Patient
     │
     ├── Vital Signs
     ├── Medications
     ├── Appointments
     └── Allergies
```

This gives us the connection we need for the next Sprint steps, especially identifying which patient belongs to a logged-in account.

---

## Day 1 Status

* [x] Sprint planning completed
* [x] Existing Identity setup reviewed
* [x] Patient linked to Identity user
* [x] EF Core relationship added
* [x] Migration created and reviewed
* [x] Migration applied successfully
* [x] Domain roles planned: `Patient` and `Admin`

**Day 1 completed.**
