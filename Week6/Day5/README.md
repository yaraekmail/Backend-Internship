# Week 6 — Day 5

## Sprint Review, API Demo & Retrospective

## Overview

Day 5 focused on closing Sprint 1 by demonstrating the completed API functionality, reviewing the sprint requirements, identifying remaining work, and completing the Sprint 1 retrospective.

The API demonstrations were performed using **Swagger** against the running Cardiac Patient Monitoring API.

---

## 1. Sprint 1 API Demonstration

The completed Sprint 1 API features were demonstrated through Swagger.

### Patients Catalog

The Patients catalog endpoint was tested with:

* Pagination
* Sorting
* Gender filtering
* City filtering
* Combined filtering, sorting, and pagination
* Total result count

Example endpoint:

`GET /api/Patients`

The endpoint successfully returned the requested patient data according to the supplied query parameters.

---

## 2. Medication Order Demonstration

The medication order workflow was demonstrated end-to-end.

### Request

A medication order was created for an existing patient using:

`POST /api/MedicationOrders`

The test used:

* Patient: Omar Hassan
* Medication: Metoprolol 25 mg
* Quantity: 1

### Result

The request completed successfully with:

* Status: `200 OK`
* Order ID: `5`
* Unit Price: `7.75`
* Line Total: `7.75`
* Total Amount: `7.75`

The medication price was calculated on the server side using the catalog price.

---

## 3. Stock Decrement Verification

The medication stock was checked before and after the successful order.

### Before the Order

Metoprolol 25 mg:

`StockQuantity = 2`

### After the Order

Metoprolol 25 mg:

`StockQuantity = 1`

This confirmed that the medication stock was decreased after the successful order.

---

## 4. Insufficient Stock Validation

An error scenario was tested by requesting more medication than the available stock.

The request attempted to order:

* Medication: Metoprolol 25 mg
* Quantity: 2
* Available stock: 1

### Result

The API correctly rejected the request with:

`400 Bad Request`

Response:

```json
{
  "message": "Insufficient stock for medication: Metoprolol 25 mg"
}
```

This confirmed that the business logic prevents orders when the requested quantity exceeds the available stock.

---

## 5. Transaction Rollback Verification

Transaction rollback was previously verified during Day 4.

A temporary exception was intentionally triggered after the database changes were saved but before the transaction was committed.

The API returned an error and the transaction was rolled back successfully.

The temporary test exception was then removed, and the application was built successfully.

This confirmed that the medication order workflow uses a database transaction to prevent partial changes when an error occurs before commit.

---

## 6. Sprint Review

The completed Sprint 1 work was reviewed against the planned requirements.

### Completed Features

* Patients catalog with pagination.
* Patient filtering by gender.
* Patient filtering by city.
* Patient sorting.
* DTO projection.
* Medication order creation.
* Medication quantity validation.
* Insufficient stock validation.
* Server-side medication price calculation.
* Order total calculation.
* Stock decrement.
* Transaction handling and rollback.
* Optimistic concurrency handling using `RowVersion`.

### Review Result

The main Sprint 1 API requirements were completed and demonstrated successfully through Swagger using both successful and error scenarios.

---

## 7. Acceptance Criteria Review

The Sprint 1 implementation was checked against the main acceptance requirements.

| Requirement                     | Status    |
| ------------------------------- | --------- |
| Paginated catalog               | Completed |
| Filtering                       | Completed |
| Sorting                         | Completed |
| DTO projection                  | Completed |
| Successful order creation       | Completed |
| Insufficient stock handling     | Completed |
| Stock decrement                 | Completed |
| Transaction handling            | Completed |
| Error scenario testing          | Completed |
| Mentor code review and approval | Pending   |

Mentor code review and approval were not completed during Sprint 1 and were therefore carried forward as follow-up work.

---

## 8. Sprint 2 Backlog

The following items were carried forward to Sprint 2.

### 1. Complete Mentor Code Review

* Review the Sprint 1 implementation with the mentor.
* Address any feedback received during the review.

### 2. Improve Automated Tests

* Add automated tests for the medication order business logic.
* Test successful order creation.
* Test insufficient stock scenarios.
* Add coverage for transaction rollback behavior.

### 3. Continue API Improvements

* Apply improvements identified during code review.
* Continue validating the API through successful and error scenarios.

---

## 9. Sprint 1 Retrospective

### What Went Well

* The Patients catalog features were implemented and tested successfully.
* Pagination, filtering, and sorting were demonstrated through Swagger.
* The medication order workflow included real business logic beyond basic CRUD.
* Stock validation and stock decrement were tested successfully.
* Transaction handling and rollback were verified.
* Both successful and error scenarios were tested during the Sprint 1 API demonstration.

### What Could Be Improved

* Mentor code review was not completed during Sprint 1.
* More automated tests are needed for the medication order business logic.
* Code review should be included earlier in the development process.

### Concrete Action for Sprint 2

**Write automated tests for the medication order service before adding the next major write operation.**

This will help detect business logic and transaction-related issues earlier and make the implementation easier to review.

---

## 10. Sprint 1 Summary

Day 5 completed the Sprint 1 close-out activities.

The API functionality was demonstrated through Swagger, successful and error scenarios were verified, remaining work was moved to the Sprint 2 backlog, and the Sprint 1 retrospective was completed.

### Final Status

**Sprint 1 — Completed**

The main Sprint 1 API requirements were implemented and demonstrated successfully.

Mentor code review and approval remain as follow-up work for Sprint 2.
