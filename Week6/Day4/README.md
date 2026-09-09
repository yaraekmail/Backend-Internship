# Week 6 – Day 4: Write Operations & Business Logic

## Overview

Implemented a medication order workflow for the Cardiac Patient Monitoring API, including business rules, stock management, database transactions, and concurrency handling.

## What Was Added

### Medication Order

Added a complete medication ordering workflow:

* Validates that the patient exists.
* Validates medication quantities.
* Checks medication availability in stock.
* Calculates each line total from the server-side medication price.
* Calculates the complete order total.
* Decreases medication stock after a successful order.
* Uses a database transaction to keep order creation and stock changes atomic.
* Handles concurrent stock updates using optimistic concurrency with `RowVersion`.

### New Entities

* [MedicationCatalogItem](CardiacPatientMonitoring.Api/Entities/MedicationCatalogItem.cs) — medication catalog, price, and stock.
* [MedicationOrder](CardiacPatientMonitoring.Api/Entities/MedicationOrder.cs) — patient medication order.
* [MedicationOrderItem](CardiacPatientMonitoring.Api/Entities/MedicationOrderItem.cs) — medications and quantities included in an order.

### DTOs

* [CreateMedicationOrderRequest](CardiacPatientMonitoring.Api/DTOs/CreateMedicationOrderRequest.cs) — order creation request.
* [MedicationOrderResponse](CardiacPatientMonitoring.Api/DTOs/MedicationOrderResponse.cs) — order response.

### Business Logic

* [IMedicationOrderService](CardiacPatientMonitoring.Api/Services/IMedicationOrderService.cs)
* [MedicationOrderService](CardiacPatientMonitoring.Api/Services/MedicationOrderService.cs)

The service contains the validation, stock checking, price calculations, order creation, stock updates, transaction handling, and concurrency handling.

### API Endpoint

* [MedicationOrdersController](CardiacPatientMonitoring.Api/Controllers/MedicationOrdersController.cs)

**Endpoint:**

`POST /api/MedicationOrders`

### Database

Updated the [CardiacPatientMonitoringDbContext](CardiacPatientMonitoring.Api/Data/CardiacPatientMonitoringDbContext.cs) with the medication catalog and order relationships.

Added EF Core migrations for:

* Medication orders and order items.
* Medication catalog concurrency handling.

### Seed Data

Added medication catalog sample data through [SeedData](CardiacPatientMonitoring.Api/Data/SeedData.cs).

## Testing

The implementation was tested through Swagger:

* Successful medication order
* Insufficient stock rejection
* Transaction rollback
* Successful transaction after removing the temporary rollback test
* Build verification

## Verification

The project builds successfully with `dotnet build`.

## Documentation

Detailed task documentation and testing screenshots are included in the Day 4 documentation.

## Status

**Day 4 — Completed ✅**
