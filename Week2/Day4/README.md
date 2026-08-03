# Day 4 - ASP.NET Core Web API Documentation

## Overview

This project demonstrates the concepts learned on Day 4 of the Backend Internship. The focus was on creating an ASP.NET Core Web API project, building RESTful endpoints, documenting APIs, and understanding the differences between Controllers and Minimal APIs.

## Learning Objectives

- Create and configure an ASP.NET Core Web API project.
- Build RESTful API endpoints.
- Test APIs using Postman.
- Generate API documentation with Swagger/OpenAPI.
- Understand the differences between Controllers and Minimal APIs.

## Topics Covered

- ASP.NET Core Web API
- REST API Fundamentals
- HTTP Methods (GET, POST, PUT, DELETE)
- Route Parameters
- Query Parameters
- Request Body
- Action Results
- Swagger / OpenAPI
- Controllers vs Minimal APIs

## Tasks Completed

### Task 1 - Project Setup

- Created a new ASP.NET Core Web API project.
- Verified the application runs successfully.
- Tested the default WeatherForecast endpoint.

### Task 2 - Build REST Endpoints

Implemented CRUD endpoints for a sample resource:

- GET
- GET by Id
- POST
- PUT
- DELETE

### Task 3 - Test with Postman

- Sent requests to all endpoints.
- Verified request and response behavior.
- Tested different HTTP methods.

### Task 4 - API Documentation

- Enabled Swagger/OpenAPI.
- Explored API endpoints using Swagger UI.
- Tested endpoints directly from Swagger.

### Task 5 - Controllers vs Minimal APIs

| Controllers                          | Minimal APIs                            |
| ------------------------------------ | --------------------------------------- |
| Better for medium and large projects | Best for small projects                 |
| Organized into controller classes    | Everything can be defined in Program.cs |
| Easier to maintain                   | Less boilerplate                        |
| Supports attributes                  | Simple route mapping                    |
| Better scalability                   | Faster for prototypes                   |

## Tools Used

- .NET SDK
- ASP.NET Core Web API
- Visual Studio Code
- Swagger / OpenAPI
- Postman

## Outcome

By completing Day 4, I gained practical experience in building and documenting RESTful APIs using ASP.NET Core, testing endpoints with Postman, and understanding when to use Controllers versus Minimal APIs.
