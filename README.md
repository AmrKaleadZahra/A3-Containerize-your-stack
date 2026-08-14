# Containerize your stack
# ASP.NET Core Tasks API

A RESTful Web API built with **ASP.NET Core** and **Entity Framework Core** for managing tasks. The project demonstrates CRUD operations, filtering, searching, database integration with SQL Server, dependency injection, and clean API design.

## Features

* Create a task
* Get all tasks
* Get a task by ID
* Update a task
* Delete a task
* Filter tasks by completion status
* Search tasks by title
* Reset tasks to seed data
* Entity Framework Core with SQL Server
* Dependency Injection
* Async programming with `async`/`await`
* Swagger/OpenAPI support
* Dockerfile Support
* Docker Compose

## Technologies

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI

## Getting Started

### Prerequisites

* .NET 8 SDK (or your project's target framework)
* SQL Server

### Installation

Clone the repository:

```bash
git clone https://github.com/<your-username>/<your-repository>.git
cd <your-repository>
```

Restore dependencies:

```bash
dotnet restore
```

Update the connection string in `appsettings.Development.json`.

Apply database migrations:

```bash
dotnet ef database update
```

Run the project:

```bash
dotnet run
```

The API will be available at:

```
https://localhost:xxxx
```

Swagger UI:

```
https://localhost:xxxx/swagger
```

---

# API Endpoints

| Method | Endpoint                              | Description                   |
| ------ | ------------------------------------- | ----------------------------- |
| GET    | `/api/tasks`                          | Get all tasks                 |
| GET    | `/api/tasks/{id}`                     | Get a task by ID              |
| GET    | `/api/tasks?done=true`                | Filter by completion status   |
| GET    | `/api/tasks?search=keyword`           | Search by title               |
| GET    | `/api/tasks?done=true&search=keyword` | Filter and search together    |
| POST   | `/api/task`                           | Create a new task             |
| PUT    | `/api/task/{id}`                      | Update an existing task       |
| DELETE | `/api/task/{id}`                      | Delete a task                 |
| POST   | `/api/task/reset`                     | Reset database with seed data |

---

# Request Examples

## Create Task

**POST** `/api/task`

```json
{
  "title": "Learn Entity Framework Core"
}
```

## Update Task

**PUT** `/api/task/1`

```json
{
  "title": "Learn ASP.NET Core",
  "done": true
}
```

---

# Sample Response

```json
{
  "id": 1,
  "title": "Learn ASP.NET Core",
  "done": false
}
```

---

# Project Structure

```
TasksApi
│
├── Controllers
├── Services
├── Data
├── Models
├── DTOs
├── Migrations
├── Program.cs
└── appsettings.json
```

---

# Future Improvements

* Authentication & Authorization (JWT)
* Pagination
* Sorting
* Validation using FluentValidation
* Global Exception Handling Middleware
* Repository Pattern
* Unit Testing
* Docker Support

---

# License

This project is intended for learning and demonstration purposes.
