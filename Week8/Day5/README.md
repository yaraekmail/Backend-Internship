# Week 8 — Day 5: Sprint Review, Benchmark Demo & Retrospective

## 1. Overview

Day 5 focused on closing Sprint 3 by reviewing the completed performance work, presenting the measured before-and-after results, checking the Sprint 3 backlog, and defining improvements for Sprint 4.

The review was based on the actual work completed during Days 1–4 and the recorded performance evidence.

The main areas reviewed were:

* N+1 query investigation and optimization
* EF Core eager loading and projection
* Redis caching and cache invalidation
* Database indexing and SQL performance measurements

---

## 2. Day 5 Objectives

The main objectives were:

* Review the performance improvements completed during Sprint 3.
* Present measurable before-and-after evidence.
* Check the Sprint 3 work against its planned goals.
* Identify any remaining performance opportunities for Sprint 4.
* Write a Sprint 3 retrospective.
* Define one concrete action for Sprint 4.

---

## 3. Sprint 3 Performance Review

Sprint 3 focused on improving API and database performance through query optimization, caching, and indexing.

The completed work was reviewed using the actual measurements collected during the sprint.

### Day 1 — N+1 Investigation

The existing endpoints were reviewed first to determine whether an actual N+1 problem existed.

No genuine pre-existing N+1 problem was found in the existing endpoints.

An experimental endpoint was then created to intentionally demonstrate the N+1 pattern.

For the experimental Patient-Medication access pattern:

```text
51 Patients
52 database commands
```

The optimized version reduced the database work to:

```text
2 database commands
```

This demonstrated the difference between repeated queries inside a loop and a batched approach.

### Day 2 — Query Optimization

Two query optimization approaches were implemented and measured.

#### Eager Loading

The eager-loading endpoint used:

```csharp
.Include(patient => patient.Medications)
```

The resulting operation used:

```text
1 database command
```

#### Projection

The projection endpoint selected only the required fields.

The resulting operation also used:

```text
1 database command
```

`AsSplitQuery` was reviewed but was not applied because the current project did not contain a scenario with multiple collection navigations requiring split-query behavior.

---

## 4. Redis Caching Review

Day 3 introduced Redis caching for the medication catalog.

The implementation used:

* Redis through `IDistributedCache`
* Cache-aside behavior
* A 10-minute absolute expiration
* Cache key: `medication-catalog:all`
* Cache invalidation after create, update, and delete operations

### Cache Miss

The first catalog request loaded the data from SQL Server and stored it in Redis.

Measured elapsed time:

```text
864 ms
```

### Cache Hits

Subsequent requests were served from the cache.

Measured elapsed times:

```text
83 ms
64 ms
63 ms
```

These measurements demonstrated the difference observed between the initial cache miss and subsequent cache hits.

### Cache Invalidation

Cache invalidation was tested after:

* Creating a catalog item
* Updating a catalog item
* Deleting a catalog item

After each write operation, the cached catalog was invalidated and the following GET request loaded the updated data from SQL Server.

This confirmed that the catalog did not continue serving stale cached data after the tested write operations.

---

## 5. Database Indexing Review

Day 4 focused on identifying query patterns that filter by `PatientId` and sort by another column.

Three composite indexes were added:

```text
Appointments:
PatientId + AppointmentDate

Medications:
PatientId + StartDate

VitalSigns:
PatientId + RecordedAt
```

The indexes were added through EF Core Fluent API and applied through the migration:

```text
20260915071147_AddPerformanceIndexes
```

### Before vs After SQL Measurements

| Query        | Before | After | Observed Change |
| ------------ | -----: | ----: | --------------: |
| Appointments |   5 ms |  2 ms |      3 ms lower |
| Medications  |   4 ms |  1 ms |      3 ms lower |
| VitalSigns   |   3 ms |  1 ms |      2 ms lower |

The SQL execution time decreased in all three recorded test runs.

However, these measurements were collected on a small local development dataset. Therefore, they are treated as observed test results rather than production-scale benchmark results.

The API elapsed times were not used as the primary indexing evidence because they include application-level overhead in addition to SQL execution.

No execution-plan result was recorded, so no claim is made about a specific SQL Server execution-plan operator such as Index Seek or Table Scan.

---

## 6. Sprint 3 Before-and-After Summary

The main performance evidence collected during Sprint 3 was:

| Area                     | Before             | After                      |
| ------------------------ | ------------------ | -------------------------- |
| Experimental N+1 pattern | 52 DB commands     | 2 DB commands              |
| Eager loading            | —                  | 1 DB command               |
| Projection               | —                  | 1 DB command               |
| Redis catalog            | 864 ms cache miss  | 83 / 64 / 63 ms cache hits |
| Appointments index       | 5 ms SQL execution | 2 ms                       |
| Medications index        | 4 ms SQL execution | 1 ms                       |
| VitalSigns index         | 3 ms SQL execution | 1 ms                       |

The results show that Sprint 3 produced measurable improvements and practical demonstrations of query optimization, caching, and indexing.

---

## 7. Sprint Review — Planned Work vs Actual Work

The Sprint 3 plan was reviewed against the work completed during Days 1–4.

### Query Investigation

**Planned:** Identify N+1 query patterns and measure database commands.

**Completed:** Existing endpoints were reviewed, no genuine pre-existing N+1 was found, and an experimental N+1 endpoint was created and measured.

**Status:** Completed.

### Query Optimization

**Planned:** Apply appropriate EF Core loading and projection techniques.

**Completed:** Eager loading and projection were implemented and both demonstrated one database command.

**Status:** Completed.

### Redis Caching

**Planned:** Add caching for suitable catalog data and invalidate the cache after writes.

**Completed:** Redis caching was implemented for the medication catalog, and invalidation was tested after create, update, and delete operations.

**Status:** Completed.

### Performance Measurement

**Planned:** Collect measurable performance evidence.

**Completed:** SQL query counts, cache timings, and SQL execution times were recorded throughout Sprint 3.

**Status:** Completed.

### Database Indexing

**Planned:** Identify suitable indexes, add composite indexes, apply a migration, and compare measurements.

**Completed:** Three composite indexes were added, migrated, applied to the database, and measured before and after.

**Status:** Completed.

---

## 8. Remaining Opportunities for Sprint 4

The Sprint 3 work met the planned implementation goals, but two performance-related opportunities were identified for future work.

### 8.1 Automated Query Regression Testing

The N+1 investigation relied on observing actual database commands during development.

A useful Sprint 4 improvement is to add an automated test that protects important query behavior and helps detect an N+1 regression if future code changes introduce repeated database queries.

### 8.2 Larger-Scale Index Profiling

The Day 4 index measurements were collected using the current local development dataset.

A future performance investigation could use a larger dataset and execution-plan analysis to provide stronger evidence about how SQL Server executes the indexed queries.

These are future opportunities and were not treated as incomplete Sprint 3 implementation work.

---

## 9. Sprint 3 Retrospective

### What Went Well

The sprint followed a measurement-based approach instead of relying only on assumptions about performance.

The N+1 investigation showed the importance of counting actual database commands.

The Redis work included testing both cache hits and cache invalidation after write operations.

The indexing work was based on actual query patterns from the existing controllers rather than adding indexes indiscriminately.

The performance results were documented using the measurements that were actually observed.

### What Could Be Improved

The performance measurements were collected on a small local development database, which limits how strongly the results can be generalized.

For database indexing, execution-plan evidence could provide additional information about how SQL Server processes the queries.

The query-count investigation was performed manually during development, so it would be useful to protect important query behavior with automated tests.

### Concrete Sprint 4 Action

**Add an automated regression test for important query behavior to help prevent N+1 regressions.**

This converts an important lesson from Sprint 3 into a repeatable automated check.

---

## 10. Sprint 3 Outcome

Sprint 3 successfully completed its planned performance-focused implementation work.

During the sprint:

* N+1 query behavior was investigated and demonstrated experimentally.
* The optimized approach reduced the experimental pattern from 52 database commands to 2.
* Eager loading and projection were demonstrated using one database command each.
* Redis caching was implemented for the medication catalog.
* Cache hit and cache invalidation behavior were tested.
* Three composite database indexes were added and applied through EF Core migration.
* Before-and-after SQL execution measurements were recorded for the indexed queries.

The sprint also identified clear opportunities for Sprint 4, particularly automated query regression testing and deeper profiling with larger datasets.

The documented evidence provides the basis for the Sprint 3 review and retrospective.
