# ❤️ Cardiac Patient Monitoring System

An **ASP.NET Core Web API** for managing synthetic cardiac patient data, including patient profiles, vital signs, medications, appointments, medical conditions, and allergies.

The project implements database management with **Entity Framework Core and SQL Server**, authentication using **ASP.NET Core Identity and JWT**, role-based authorization, request validation using **FluentValidation**, and interactive API documentation and testing through **Swagger/OpenAPI**.

---

## 📌 Table of Contents

* [Project Overview](#-project-overview)
* [Features](#-features)
* [Technologies](#-technologies)
* [Project Structure](#-project-structure)
* [Entities](#-entities)
* [DTOs](#-dtos)
* [Authentication](#-authentication)
* [Authorization](#-authorization)
* [Validation](#-validation)
* [Database & Entity Framework Core](#-database--entity-framework-core)
* [Database Seed Data](#-database-seed-data)
* [API Endpoints](#-api-endpoints)
* [Swagger / OpenAPI](#-swagger--openapi)
* [Documentation](#-documentation)
* [Running the Project](#-running-the-project)
* [Project Status](#-project-status)

---

# 📖 Project Overview

The **Cardiac Patient Monitoring System** is a backend API designed to manage synthetic cardiac patient information.

The system provides API functionality for:

* 👤 Patient profiles
* ❤️ Vital-sign measurements
* 💊 Medications
* 📅 Appointments
* 🩺 Medical conditions
* ⚠️ Allergies

The API uses Entity Framework Core to communicate with SQL Server and provides protected endpoints using JWT authentication and role-based authorization.

---

# ✨ Features

## 👤 Patient Management

The API manages patient information including:

* First name
* Last name
* Date of birth
* Gender
* Phone
* Email
* Address
* City
* State

**Implementation:**

* [PatientsController.cs](Controllers/PatientsController.cs)
* [Patient.cs](Entities/Patient.cs)
* [CreatePatientRequest.cs](DTOs/CreatePatientRequest.cs)
* [UpdatePatientRequest.cs](DTOs/UpdatePatientRequest.cs)
* [PatientResponse.cs](DTOs/PatientResponse.cs)

---

## ❤️ Vital Sign Management

Vital-sign records are associated with patients and include:

* Heart rate
* Systolic blood pressure
* Diastolic blood pressure
* Oxygen saturation
* Temperature
* Respiratory rate
* Recorded date and time

**Implementation:**

* [VitalSignsController.cs](Controllers/VitalSignsController.cs)
* [VitalSign.cs](Entities/VitalSign.cs)
* [CreateVitalSignRequest.cs](DTOs/CreateVitalSignRequest.cs)
* [UpdateVitalSignRequest.cs](DTOs/UpdateVitalSignRequest.cs)
* [VitalSignResponse.cs](DTOs/VitalSignResponse.cs)

The API also supports retrieving vital signs for a specific patient.

---

## 💊 Medication Management

Medication records are associated with patients and include medication information such as:

* Medication name
* Dosage
* Frequency
* Start date

**Implementation:**

* [MedicationsController.cs](Controllers/MedicationsController.cs)
* [Medication.cs](Entities/Medication.cs)
* [CreateMedicationRequest.cs](DTOs/CreateMedicationRequest.cs)
* [UpdateMedicationRequest.cs](DTOs/UpdateMedicationRequest.cs)
* [MedicationResponse.cs](DTOs/MedicationResponse.cs)

---

## 📅 Appointment Management

Appointment records are associated with patients and include:

* Appointment date
* Reason
* Status
* Notes

**Implementation:**

* [AppointmentsController.cs](Controllers/AppointmentsController.cs)
* [Appointment.cs](Entities/Appointment.cs)
* [CreateAppointmentRequest.cs](DTOs/CreateAppointmentRequest.cs)
* [UpdateAppointmentRequest.cs](DTOs/UpdateAppointmentRequest.cs)
* [AppointmentResponse.cs](DTOs/AppointmentResponse.cs)

---

## 🩺 Medical Conditions

Medical conditions are associated with patients and include:

* Condition name
* Diagnosis date
* Notes

**Implementation:**

* [MedicalCondition.cs](Entities/MedicalCondition.cs)

---

## ⚠️ Allergies

Allergy records are associated with patients and include:

* Allergen
* Severity
* Notes

**Implementation:**

* [Allergy.cs](Entities/Allergy.cs)

---

# 🛠️ Technologies

| Technology                    | Purpose                       |
| ----------------------------- | ----------------------------- |
| **C#**                        | Backend programming language  |
| **ASP.NET Core Web API**      | API development               |
| **.NET 10**                   | Application framework         |
| **Entity Framework Core**     | ORM and database access       |
| **SQL Server**                | Database                      |
| **ASP.NET Core Identity**     | User and role management      |
| **JWT Bearer Authentication** | Authentication                |
| **FluentValidation**          | Request validation            |
| **Swagger / OpenAPI**         | API documentation and testing |
| **EF Core Migrations**        | Database schema management    |

---

# 📂 Project Structure

```text
CardiacPatientMonitoring.Api
│
├── Controllers
│   ├── AppointmentsController.cs
│   ├── AuthController.cs
│   ├── MedicationsController.cs
│   ├── PatientsController.cs
│   └── VitalSignsController.cs
│
├── Data
│   ├── CardiacPatientMonitoringDbContext.cs
│   ├── IdentitySeeder.cs
│   └── SeedData.cs
│
├── DTOs
│   ├── AppointmentResponse.cs
│   ├── CreateAppointmentRequest.cs
│   ├── CreateMedicationRequest.cs
│   ├── CreatePatientRequest.cs
│   ├── CreateVitalSignRequest.cs
│   ├── MedicationResponse.cs
│   ├── PatientResponse.cs
│   ├── UpdateAppointmentRequest.cs
│   ├── UpdateMedicationRequest.cs
│   ├── UpdatePatientRequest.cs
│   ├── UpdateVitalSignRequest.cs
│   └── VitalSignResponse.cs
│
├── Entities
│   ├── Allergy.cs
│   ├── Appointment.cs
│   ├── MedicalCondition.cs
│   ├── Medication.cs
│   ├── Patient.cs
│   └── VitalSign.cs
│
├── Migrations
│   ├── 20260826203005_InitialCreate.cs
│   ├── 20260826203005_InitialCreate.Designer.cs
│   └── CardiacPatientMonitoringDbContextModelSnapshot.cs
│
├── Models
│   ├── LoginRequest.cs
│   └── RegisterRequest.cs
│
├── Validators
│   ├── MedicationAppointmentValidators.cs
│   ├── PatientValidators.cs
│   └── VitalSignValidators.cs
│
├── Properties
│   └── launchSettings.json
│
├── Program.cs
├── CardiacPatientMonitoring.Api.csproj
├── CardiacPatientMonitoring.Api.http
├── appsettings.json
└── appsettings.Development.json
```

---

# 🧩 Entities

The database domain is represented by six main entities:

| Entity            | Code                                                |
| ----------------- | --------------------------------------------------- |
| Patient           | [Patient.cs](Entities/Patient.cs)                   |
| Vital Sign        | [VitalSign.cs](Entities/VitalSign.cs)               |
| Medication        | [Medication.cs](Entities/Medication.cs)             |
| Appointment       | [Appointment.cs](Entities/Appointment.cs)           |
| Medical Condition | [MedicalCondition.cs](Entities/MedicalCondition.cs) |
| Allergy           | [Allergy.cs](Entities/Allergy.cs)                   |

---

# 📦 DTOs

The project separates API request/response models from database entities.

### Patient DTOs

* [CreatePatientRequest.cs](DTOs/CreatePatientRequest.cs)
* [UpdatePatientRequest.cs](DTOs/UpdatePatientRequest.cs)
* [PatientResponse.cs](DTOs/PatientResponse.cs)

### Vital Sign DTOs

* [CreateVitalSignRequest.cs](DTOs/CreateVitalSignRequest.cs)
* [UpdateVitalSignRequest.cs](DTOs/UpdateVitalSignRequest.cs)
* [VitalSignResponse.cs](DTOs/VitalSignResponse.cs)

### Medication DTOs

* [CreateMedicationRequest.cs](DTOs/CreateMedicationRequest.cs)
* [UpdateMedicationRequest.cs](DTOs/UpdateMedicationRequest.cs)
* [MedicationResponse.cs](DTOs/MedicationResponse.cs)

### Appointment DTOs

* [CreateAppointmentRequest.cs](DTOs/CreateAppointmentRequest.cs)
* [UpdateAppointmentRequest.cs](DTOs/UpdateAppointmentRequest.cs)
* [AppointmentResponse.cs](DTOs/AppointmentResponse.cs)

---

# 🔐 Authentication

Authentication is implemented using **ASP.NET Core Identity** and **JWT Bearer Authentication**.

The authentication controller is:

* [AuthController.cs](Controllers/AuthController.cs)

Authentication request models:

* [LoginRequest.cs](Models/LoginRequest.cs)
* [RegisterRequest.cs](Models/RegisterRequest.cs)

JWT authentication configuration is implemented in:

* [Program.cs](Program.cs)

The API validates the JWT issuer, audience, signing key, and token lifetime.

---

# 👑 Authorization

The application uses role-based authorization with two roles:

```text
User
Admin
```

Identity configuration and role management are implemented through:

* [Program.cs](Program.cs)
* [IdentitySeeder.cs](Data/IdentitySeeder.cs)

The default local development Admin account is created by the Identity Seeder.

Authorization was verified through Swagger, including:

* `401 Unauthorized`
* `403 Forbidden`

Administrative delete operations were tested using the Admin role, while non-admin access was tested and returned `403 Forbidden`.

---

# ✅ Validation

Request validation is implemented using **FluentValidation**.

The validators are located in:

* [PatientValidators.cs](Validators/PatientValidators.cs)
* [VitalSignValidators.cs](Validators/VitalSignValidators.cs)
* [MedicationAppointmentValidators.cs](Validators/MedicationAppointmentValidators.cs)

Validator registration is configured in:

* [Program.cs](Program.cs)

Invalid requests were tested through Swagger and returned validation errors with:

```text
400 Bad Request
```

---

# 🗄️ Database & Entity Framework Core

The project uses **Entity Framework Core with SQL Server**.

The database context is:

* [CardiacPatientMonitoringDbContext.cs](Data/CardiacPatientMonitoringDbContext.cs)

The context inherits from:

```text
IdentityDbContext<IdentityUser>
```

and contains DbSets for:

* Patients
* Vital Signs
* Medications
* Appointments
* Medical Conditions
* Allergies

The entity relationships are configured in the database context.

Each related record is associated with a patient through a foreign key.

The project also contains the initial EF Core migration:

* [InitialCreate Migration](Migrations/20260826203005_InitialCreate.cs)
* [Migration Designer](Migrations/20260826203005_InitialCreate.Designer.cs)
* [Model Snapshot](Migrations/CardiacPatientMonitoringDbContextModelSnapshot.cs)

---

# 🌱 Database Seed Data

Initial synthetic cardiac data is created through:

* [SeedData.cs](Data/SeedData.cs)

The seed process checks whether patients already exist before inserting the sample data, preventing duplicate seed insertion.

The seeded data includes:

* **5 Patients**
* **15 Vital Signs**
* **5 Medications**
* **5 Appointments**
* **5 Medical Conditions**
* **5 Allergies**

The related records use fixed patient IDs so that the relationships between patients and their associated data are maintained.

Identity seed data is handled separately through:

* [IdentitySeeder.cs](Data/IdentitySeeder.cs)

---

# 🔗 API Endpoints

## Authentication

| Method | Endpoint             | Purpose                       |
| ------ | -------------------- | ----------------------------- |
| POST   | `/api/Auth/register` | Register a user               |
| POST   | `/api/Auth/login`    | Authenticate and obtain a JWT |

**Controller:** [AuthController.cs](Controllers/AuthController.cs)

---

## Patients

| Method | Endpoint             | Purpose             |
| ------ | -------------------- | ------------------- |
| GET    | `/api/Patients`      | Get all patients    |
| GET    | `/api/Patients/{id}` | Get a patient by ID |
| POST   | `/api/Patients`      | Create a patient    |
| PUT    | `/api/Patients/{id}` | Update a patient    |
| DELETE | `/api/Patients/{id}` | Delete a patient    |

**Controller:** [PatientsController.cs](Controllers/PatientsController.cs)

---

## Vital Signs

| Method | Endpoint                              | Purpose                       |
| ------ | ------------------------------------- | ----------------------------- |
| GET    | `/api/VitalSigns`                     | Get all vital signs           |
| GET    | `/api/VitalSigns/{id}`                | Get a vital sign by ID        |
| GET    | `/api/VitalSigns/patient/{patientId}` | Get vital signs for a patient |
| POST   | `/api/VitalSigns`                     | Create a vital sign           |
| PUT    | `/api/VitalSigns/{id}`                | Update a vital sign           |
| DELETE | `/api/VitalSigns/{id}`                | Delete a vital sign           |

**Controller:** [VitalSignsController.cs](Controllers/VitalSignsController.cs)

---

## Medications

| Method | Endpoint                | Purpose              |
| ------ | ----------------------- | -------------------- |
| GET    | `/api/Medications`      | Get medications      |
| GET    | `/api/Medications/{id}` | Get medication by ID |
| POST   | `/api/Medications`      | Create medication    |
| PUT    | `/api/Medications/{id}` | Update medication    |
| DELETE | `/api/Medications/{id}` | Delete medication    |

**Controller:** [MedicationsController.cs](Controllers/MedicationsController.cs)

---

## Appointments

| Method | Endpoint                 | Purpose               |
| ------ | ------------------------ | --------------------- |
| GET    | `/api/Appointments`      | Get appointments      |
| GET    | `/api/Appointments/{id}` | Get appointment by ID |
| POST   | `/api/Appointments`      | Create appointment    |
| PUT    | `/api/Appointments/{id}` | Update appointment    |
| DELETE | `/api/Appointments/{id}` | Delete appointment    |

**Controller:** [AppointmentsController.cs](Controllers/AppointmentsController.cs)

---

# 📚 Swagger / OpenAPI

Swagger is configured in:

* [Program.cs](Program.cs)

The project provides interactive API documentation and allows endpoints to be tested directly through Swagger UI.

JWT Bearer authentication is also configured for Swagger so that protected endpoints can be tested with an access token.

---

# 🧪 Testing & Verification

The API was tested through Swagger UI.

The testing documentation covers:

### Authentication

* Successful registration
* Duplicate registration
* Successful login
* Authentication failure
* JWT authorization

### Patients

* Get all patients
* Get existing patient
* Patient not found
* Successful patient creation
* Invalid patient request
* Successful patient update
* Patient update validation error
* Admin delete
* Non-admin `403 Forbidden`

### Vital Signs

* Get all vital signs
* Get existing vital sign
* Vital sign not found
* Get patient vital signs
* Successful vital-sign creation
* Vital-sign validation error
* Successful vital-sign update
* Vital-sign validation error
* Admin delete
* Non-admin `403 Forbidden`

### Seed Data

Swagger was used to verify seeded:

* Patient data
* Vital-sign data
* Medication data
* Appointment data

---

# 📸 Documentation

The project documentation is stored inside the repository under the `docs` folder.

### Implementation Documentation

[Cardiac Patient Monitoring System — Backend Documentation](docs/Cardiac_Patient_Monitoring_System_backendp1.docx)

### Swagger Testing Documentation

[Cardiac Patient Monitoring System — Swagger Testing Documentation](docs/Cardiac%20Patient%20Monitoring%20System-SWAGGER%20TESTING%20DOCUMENTATION_Formatted.docx)

The documentation includes screenshots demonstrating the implemented API functionality and Swagger testing results.

---

# 🚀 Running the Project

## 1. Clone the Repository

```bash
git clone <repository-url>
```

## 2. Navigate to the API Project

```bash
cd CardiacPatientMonitoring.Api
```

## 3. Configure the Database

Configure the SQL Server connection string using:

* [appsettings.json](appsettings.json)
* [appsettings.Development.json](appsettings.Development.json)

The configured connection uses:

```text
DefaultConnection
```

## 4. Configure JWT Settings

Configure the required JWT settings:

```text
Jwt:Key
Jwt:Issuer
Jwt:Audience
```

The JWT key should be stored securely through the appropriate local development configuration.

## 5. Apply the Database Migration

```bash
dotnet ef database update
```

## 6. Run the API

```bash
dotnet run
```

When the application starts, the configured Identity roles, Admin development account, and database seed data are initialized.

---

# 📁 Important Code Links

For quick access to the main implementation files:

### Application Configuration

* [Program.cs](Program.cs)
* [CardiacPatientMonitoringDbContext.cs](Data/CardiacPatientMonitoringDbContext.cs)

### Authentication

* [AuthController.cs](Controllers/AuthController.cs)
* [LoginRequest.cs](Models/LoginRequest.cs)
* [RegisterRequest.cs](Models/RegisterRequest.cs)
* [IdentitySeeder.cs](Data/IdentitySeeder.cs)

### Database Seed

* [SeedData.cs](Data/SeedData.cs)

### Controllers

* [PatientsController.cs](Controllers/PatientsController.cs)
* [VitalSignsController.cs](Controllers/VitalSignsController.cs)
* [MedicationsController.cs](Controllers/MedicationsController.cs)
* [AppointmentsController.cs](Controllers/AppointmentsController.cs)

### Validators

* [PatientValidators.cs](Validators/PatientValidators.cs)
* [VitalSignValidators.cs](Validators/VitalSignValidators.cs)
* [MedicationAppointmentValidators.cs](Validators/MedicationAppointmentValidators.cs)

### Entities

* [Patient.cs](Entities/Patient.cs)
* [VitalSign.cs](Entities/VitalSign.cs)
* [Medication.cs](Entities/Medication.cs)
* [Appointment.cs](Entities/Appointment.cs)
* [MedicalCondition.cs](Entities/MedicalCondition.cs)
* [Allergy.cs](Entities/Allergy.cs)

---

# 📌 Project Status

The **Cardiac Patient Monitoring System** includes the implemented backend functionality for:

* Patient management
* Vital-sign management
* Medication management
* Appointment management
* Medical condition management
* Allergy management
* Entity Framework Core database integration
* SQL Server integration
* ASP.NET Core Identity
* JWT authentication
* Role-based authorization
* FluentValidation
* Swagger/OpenAPI
* Database seed data
* EF Core migrations
* Swagger-based API verification

The implementation and API testing are documented in the project's documentation files.

---

## ❤️ Cardiac Patient Monitoring System

**Backend:** ASP.NET Core Web API
**Framework:** .NET 10
**Database:** SQL Server
**ORM:** Entity Framework Core
**Authentication:** ASP.NET Core Identity + JWT
**Validation:** FluentValidation
**API Documentation:** Swagger / OpenAPI
