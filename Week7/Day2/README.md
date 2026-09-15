# Week 7 — Day 2: JWT Login & Registration

## Overview

Day 2 focused on implementing patient registration and JWT-based login for the Cardiac Patient Monitoring API.

The registration flow creates both an ASP.NET Core Identity user and a linked Patient record. The login flow authenticates the user and issues a JWT containing domain-relevant claims, including the Patient ID and role.

## Objectives

* Link the Patient domain entity to ASP.NET Core Identity.
* Register a new patient account and create the related Patient record.
* Keep Identity user creation and Patient creation within one database transaction.
* Assign the `Patient` role to newly registered patients.
* Authenticate users using their email and password.
* Issue JWT access tokens after successful login.
* Include the linked Patient ID in the JWT claims.
* Verify the complete registration → database → login → JWT flow.

## Implementation

### 1. Patient–Identity Relationship

The `Patient` entity was linked to `IdentityUser` using an optional `UserId` foreign key.

The relationship is configured as a one-to-one relationship:

```csharp
modelBuilder.Entity<Patient>()
    .HasOne(p => p.User)
    .WithOne()
    .HasForeignKey<Patient>(p => p.UserId)
    .OnDelete(DeleteBehavior.SetNull);
```

**Code:**
[Patient.cs](./CardiacPatientMonitoring/ CardiacPatientMonitoring.Api/Entities/Patient.cs)
[CardiacPatientMonitoringDbContext.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs)

> Note: remove the space between `CardiacPatientMonitoring/` and `CardiacPatientMonitoring.Api` when pasting if GitHub doesn't resolve it.

Migration created and applied:

```text
20260910071042_AddPatientIdentityLink
```

**Migration:**
[AddPatientIdentityLink migration](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Migrations/)

### 2. Patient Registration

The registration request was expanded to include:

* Email
* Password
* First Name
* Last Name
* Date of Birth
* Gender

The registration process:

1. Checks whether the email is already registered.
2. Starts a database transaction.
3. Creates the `IdentityUser`.
4. Creates the linked `Patient`.
5. Assigns the `Patient` role.
6. Saves the changes.
7. Commits the transaction.

**Code:**
[RegisterRequest.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Models/RegisterRequest.cs)
[AuthController.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)

### 3. JWT Login

The login endpoint authenticates the Identity user using the registered email and password.

After successful authentication, the API:

1. Retrieves the user's roles.
2. Finds the Patient linked to the Identity user.
3. Creates the JWT claims.
4. Adds the `patientId` claim when a linked Patient exists.
5. Adds the user's role to the JWT.
6. Signs and returns the JWT.

The JWT includes:

* `sub`
* `email`
* `jti`
* `patientId`
* `role`
* `exp`
* `iss`
* `aud`

The token expiration is configured for 15 minutes.

**Code:**
[AuthController.cs](./CardiacPatientMonitoring/CardiacPatientMonitoring.Api/Controllers/AuthController.cs)

## Testing

The registration and login flow was tested successfully using **Swagger UI**.

### Registration

A new patient account was registered successfully and returned:

```text
200 OK
User registered successfully.
```

**Screenshot:**

![Registration successful](Week7/Day2/img/Day2_Registration_Success_200OK.png.png)

### Database Verification

The registered Identity account was verified in `AspNetUsers`.

The corresponding Patient record was verified in `Patients`.

The `Patients.UserId` value matched the `AspNetUsers.Id` value, confirming the Identity–Patient relationship.

**Screenshot:**

![Identity and Patient link](Week7/Day2/img/Screenshot 2026-09-10 141809.png)

### Patient Role Verification

The registered account was verified as having the `Patient` role.

**Screenshot:**

![Patient role](Week7/Day2/img/Screenshot 2026-09-10 141748.png)

### JWT Verification

The returned JWT was decoded and verified.

The decoded payload contained:

```text
sub
email
jti
patientId
role = Patient
exp
iss
aud
```

The `patientId` value matched the `Patient.Id` stored in the database.

**Screenshot:**

![JWT decoded claims](Week7/Day2/img/Day2_JWT_Decoded_PatientId_Claims.png.png)

## Evidence Summary

| Evidence                                  | What it demonstrates                               |
| ----------------------------------------- | -------------------------------------------------- |
| `Day2_Registration_Success_200OK.png`     | Successful patient registration                    |
| `Day2_Database_Identity_Patient_Link.png` | Identity user linked to Patient                    |
| `Day2_Database_Patient_Role.png`          | `Patient` role assigned                            |
| `Day2_JWT_Decoded_PatientId_Claims.png`   | JWT contains `patientId` and authentication claims |

## Result

Day 2 successfully implemented and verified:

* Patient registration.
* Identity user creation.
* Patient record creation.
* Identity–Patient linking.
* Patient role assignment.
* JWT authentication.
* Domain-specific `patientId` JWT claim.
* End-to-end registration → database → login → JWT flow.
