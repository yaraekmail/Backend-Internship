# Week 6 – Day 3: Catalog & Read Operations

## Overview

Implemented a paginated **Patients catalog endpoint** using ASP.NET Core and Entity Framework Core.

The endpoint supports pagination, filtering, sorting, DTO projection, and efficient read-only queries.

## Implemented Endpoint

`GET /api/Patients`

### Features

* Pagination using `page` and `pageSize`
* Filtering by `gender`
* Filtering by `city`
* Sorting by `name` or `city`
* Projection to `PatientResponse` DTO
* `AsNoTracking()` for read-only queries
* `totalCount` included in the response
* Validation for pagination parameters

## Example Request

```text
GET /api/Patients?page=1&pageSize=2&sort=name&gender=Male&city=Jenin
```

## Code Changes

* [PatientsController.cs](CardiacPatientMonitoring.Api/Controllers/PatientsController.cs) — implemented pagination, filtering, sorting, and DTO projection.
* [PatientResponse.cs](CardiacPatientMonitoring.Api/DTOs/PatientResponse.cs) — response DTO used by the catalog endpoint.

## Testing

The endpoint was tested through **Swagger** using:

* Pagination
* Gender filtering
* City filtering
* Sorting
* Combined pagination, filtering, and sorting

All tested requests returned **200 OK**.

## Verification

The project was successfully built with:

```powershell
dotnet build .\CardiacPatientMonitoring.Api\CardiacPatientMonitoring.Api.csproj
```

**Build succeeded.**

## Status

**Day 3 Hands-On Lab — Completed ✅**
