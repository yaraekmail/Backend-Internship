# Week 3 - Day 1

## REST API Design Principles & Resource Modeling

### 📚 Learning Objectives

* Understand the principles that make an API RESTful.
* Apply consistent REST resource naming conventions.
* Use HTTP methods and status codes correctly.
* Understand API versioning.
* Design a resource map for a real-world domain.

---

## 🛠️ Task

The task focused on designing a REST API resource map for a **Student Personal Development & Task Management** domain.

The API was designed around resources and standard HTTP methods instead of action-based endpoints.

---

## 📂 Core Resources

The main resources identified for the API are:

* Tasks
* Trainings
* Courses
* Skills
* IslamicGoals

---

## 🔗 REST Resource Map

The `Tasks` resource was selected as the primary resource.

### Tasks Endpoints

| Method | Endpoint             | Description         | Success Status |
| ------ | -------------------- | ------------------- | -------------- |
| GET    | `/api/v1/tasks`      | Get all tasks       | 200 OK         |
| GET    | `/api/v1/tasks/{id}` | Get a specific task | 200 OK         |
| POST   | `/api/v1/tasks`      | Create a new task   | 201 Created    |
| PUT    | `/api/v1/tasks/{id}` | Update a task       | 200 OK         |
| DELETE | `/api/v1/tasks/{id}` | Delete a task       | 204 No Content |

### Error Status Codes

* **400 Bad Request** — Invalid request data.
* **404 Not Found** — The requested resource does not exist.
* **401 Unauthorized** — Authentication is required or has failed.
* **403 Forbidden** — The user is authenticated but does not have permission.

---

## 🧩 Nested Resource

A nested resource was used to represent a relationship between resources.

Example:

`GET /api/v1/trainings/{id}/participants`

This represents the participants belonging to a specific training.

---

## 🏷️ Resource Naming Conventions

The API follows REST naming conventions:

* Resources use **plural nouns**.
* HTTP verbs describe the action.
* Verbs are not included in endpoint names.
* Resource relationships can be represented using nested routes.
* API versioning is included in the URL using `/api/v1/`.

For example:

```text
GET /api/v1/tasks
```

instead of:

```text
GET /api/getTasks
```

---

## 🌐 API Versioning

The API uses **URL-based versioning**:

```text
/api/v1/
```

This allows future API versions to be introduced without breaking existing clients.

---

## 📄 Documentation

The complete Day 1 task documentation is available here:

[📄 View Week 3 Day 1 Documentation](./Week3_Day1_Task1.docx)

---

## 🎯 Skills Practiced

* REST API principles
* Resource modeling
* RESTful endpoint design
* HTTP methods
* HTTP status codes
* Nested resources
* API versioning
* API documentation

---

## 🧰 Tools Used

* Postman
* Notion
* ASP.NET Core / .NET concepts
* Microsoft Word for task documentation
