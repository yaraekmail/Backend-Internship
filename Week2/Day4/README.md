
# Day 4 — ASP.NET Core Project Setup & Routing

## Project

**AlMandoob Stone Management System**

During Day 4, I started working with **ASP.NET Core Web API** and learned how to create and configure a Web API project, define routes, and build endpoints using both **Controllers** and **Minimal APIs**.

The implementation was practiced through a small API project called **StoneApiPractice**.

---

## Learning Objectives

During this day, I practiced:

* Creating an ASP.NET Core Web API project using the .NET CLI.
* Understanding the modern minimal hosting model in `Program.cs`.
* Creating API endpoints using Controllers.
* Creating endpoints using Minimal APIs.
* Understanding routes and route parameters.
* Working with HTTP GET endpoints.
* Testing API endpoints using Postman.
* Comparing Controllers with Minimal APIs.

---

## Project Structure

The Day 4 project is organized as follows:

```text
Day4
│
├── README.md
│
├── Documentation
│   └── Day4_API_Documentation_Improved.docx
│
└── StoneApiPractice
    │
    ├── Program.cs
    ├── StoneApiPractice.csproj
    ├── StoneApiPractice.http
    │
    ├── Controllers
    │   └── BooksController.cs
    │
    ├── Models
    │   └── Book.cs
    │
    └── Properties
        └── launchSettings.json
```

The project source code is available in the [Day4 folder on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day4).

---

# 1. Creating the Web API Project

The project was created as an ASP.NET Core Web API project using the .NET CLI.

The generated project provides the basic structure needed to run an API, including `Program.cs` and the configuration required to start the application.

The project was then customized for the training task.

**Project:** [StoneApiPractice on GitHub](https://github.com/yaraekmail/Backend-Internship/tree/main/Week2/Day4/StoneApiPractice)

---

# 2. Minimal Hosting Model

ASP.NET Core uses the modern **minimal hosting model**, where application configuration and startup are handled directly inside `Program.cs`.

The main responsibilities of `Program.cs` include:

* Creating the application builder.
* Registering required services.
* Building the application.
* Configuring the application pipeline.
* Mapping API endpoints.
* Running the application.

In this project, `Program.cs` was also used to demonstrate Minimal API endpoints.

**View the implementation:** [Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/StoneApiPractice/Program.cs)

---

# 3. Creating the Book Model

A simple `Book` model was created to represent the data returned by the API.

The model is located inside the `Models` folder.

**View the model:** [Book.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/StoneApiPractice/Models/Book.cs)

---

# 4. Controllers

A Controller groups related API endpoints inside a class.

For this task, a `BooksController` was created to handle book-related endpoints.

The controller demonstrates routing and HTTP GET actions.

**View the Controller:** [BooksController.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/StoneApiPractice/Controllers/BooksController.cs)

---

# 5. GET Endpoint — Get All Books

A GET endpoint was implemented to return the available books.

Conceptually, the endpoint follows:

```text
GET /api/books
```

This endpoint demonstrates how a Controller can expose a collection of resources through an HTTP GET request.

---

# 6. GET Endpoint with Route Parameter

A second GET endpoint was created to retrieve a specific book using its ID.

The route follows the pattern:

```text
GET /api/books/{id}
```

The `{id}` part is a **route parameter**.

For example:

```text
/api/books/3
```

The ID from the URL is passed to the controller action, which uses it to find the requested book.

This demonstrates how route parameters allow an endpoint to work with a specific resource.

---

# 7. Minimal APIs

In addition to Controllers, Minimal API endpoints were also implemented directly in `Program.cs`.

The purpose was to compare the two approaches.

### Controllers

Controllers organize endpoints inside dedicated classes.

```text
BooksController
    │
    ├── GET /api/books
    └── GET /api/books/{id}
```

### Minimal APIs

Minimal APIs define endpoints directly in `Program.cs`.

```text
Program.cs
    │
    ├── GET /books
    └── GET /books/{id}
```

This provided practical experience with both approaches rather than only studying the difference theoretically.

**View the Minimal API implementation:** [Program.cs](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/StoneApiPractice/Program.cs)

---

# 8. Routes and HTTP Verbs

The main HTTP verb practiced during this task was:

### GET

`GET` is used to retrieve data from the API.

Examples from the project:

```text
GET /api/books
GET /api/books/{id}
```

The route parameter allows the API to distinguish between requesting all books and requesting one specific book.

HTTP verbs communicate the intended operation of an endpoint. In a RESTful API, common verbs include:

| HTTP Verb | Purpose             |
| --------- | ------------------- |
| GET       | Retrieve data       |
| POST      | Create data         |
| PUT       | Replace/update data |
| DELETE    | Remove data         |

During this Day 4 task, the practical implementation focused on **GET** endpoints.

---

# 9. Testing the API

The API endpoints were tested during development to verify that the routes returned the expected results.

The project also contains:

```text
StoneApiPractice.http
```

which can be used to send HTTP requests to the API during development.

**View the HTTP requests:** [StoneApiPractice.http](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/StoneApiPractice/StoneApiPractice.http)

---

# 10. API Documentation

Additional API documentation was prepared as part of the Day 4 work.

**Documentation:** [Day4_API_Documentation_Improved.docx](https://github.com/yaraekmail/Backend-Internship/blob/main/Week2/Day4/Documentation/Day4_API_Documentation_Improved.docx)

---

# Controllers vs Minimal APIs

The main difference practiced during this task can be summarized as:

| Controllers                                            | Minimal APIs                                     |
| ------------------------------------------------------ | ------------------------------------------------ |
| Endpoints are organized inside Controller classes      | Endpoints are mapped directly                    |
| Uses attributes such as HTTP verb and route attributes | Uses methods such as `MapGet()`                  |
| Provides a structured approach for larger APIs         | Provides a lightweight approach for smaller APIs |
| Keeps endpoint logic organized by resource             | Can keep simple endpoints concise                |

For this project, both approaches were implemented to understand how they work and when each style can be useful.

---

# Skills Practiced

During Day 4, I practiced:

* Creating ASP.NET Core Web API projects.
* Using the .NET CLI.
* Understanding the minimal hosting model.
* Working with `Program.cs`.
* Creating Controllers.
* Creating Minimal API endpoints.
* Defining routes.
* Using route parameters.
* Working with HTTP GET.
* Testing API endpoints.
* Comparing Controllers and Minimal APIs.

---

# Day 4 Outcome

By the end of Day 4, I had created my first ASP.NET Core Web API project and implemented API endpoints using both **Controllers** and **Minimal APIs**.

The project provided practical experience with routing, route parameters, HTTP verbs, and the structure of an ASP.NET Core Web API.

This work forms the foundation for the more advanced API development tasks covered in the following days.
