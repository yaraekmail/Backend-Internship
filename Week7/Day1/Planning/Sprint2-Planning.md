# Week 7 — Sprint 2 Planning

## Sprint Goal

Implement real authentication and role-based access control in the Cardiac Patient Monitoring API, while building on the existing Identity and JWT setup from previous work.

## Sprint 2 Backlog

### Carry-over from Sprint 1
- Write automated tests for MedicationOrderService.
- Cover successful order creation, insufficient stock, and transaction rollback.

### Authentication & Identity
- Review and document the existing ASP.NET Core Identity integration.
- Review the existing JWT registration and login implementation.
- Link the Patient domain entity with the corresponding Identity user.
- Review the database changes required for the Patient–Identity relationship.

### Role-Based Access Control
- Define at least two domain-relevant roles.
- Use the existing User and Admin roles as the current role structure.
- Document which endpoints require authentication and which require the Admin role.
- Apply or improve role-based authorization where required.

### Resource-Based Authorization
- Ensure authenticated users can access only their own patient-related data.
- Use the Patient–Identity relationship to support ownership checks.

### Custom Middleware
- Implement a custom middleware for a genuine cross-cutting concern.
- Verify that it works correctly with the existing API pipeline.

### Testing & Documentation
- Test authentication and authorization scenarios.
- Test role-based access.
- Test resource ownership restrictions.
- Document Sprint 2 work and results.

## Existing Identity Status

The current project already uses:

- IdentityDbContext<IdentityUser>
- ASP.NET Core Identity
- IdentityRole
- JWT Bearer authentication
- User and Admin roles
- Authentication and authorization middleware
- Identity role and admin seeding

Therefore, Sprint 2 will build on the existing implementation instead of recreating it.

## Current Domain–Identity Gap

The Patient entity does not currently contain a link to the Identity user.

This relationship must be designed and implemented so that resource-based authorization can identify which patient data belongs to the authenticated user.

## Current Role Structure

### User

Regular authenticated user with access to permitted authenticated API operations.

### Admin

Administrative role with access to endpoints protected by the Admin role.

The final endpoint permissions will be reviewed and documented during the sprint.

## Five-Day Plan

### Day 1 — Sprint Planning & Identity
- Finalize Sprint 2 goal and backlog.
- Review the existing Identity integration.
- Plan the Patient–Identity relationship.
- Define roles and endpoint permissions.

### Day 2 — JWT Registration & Login
- Review and complete registration and login.
- Verify JWT generation.
- Verify domain-relevant claims.

### Day 3 — Role-Based Access Control
- Apply RBAC using the defined roles.
- Test authorized and unauthorized requests.
- Implement resource-based authorization for patient-owned data.

### Day 4 — Custom Middleware & Mentor Review
- Implement the required custom middleware.
- Test the middleware.
- Prepare the code for mentor review through GitHub PR.

### Day 5 — Sprint Review & Retrospective
- Demonstrate authentication and authorization.
- Test the required scenarios using Postman.
- Review Sprint 2 acceptance criteria.
- Document completed work and retrospective actions.

