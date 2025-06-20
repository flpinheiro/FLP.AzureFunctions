# FLP.AzureFunctions

## Overview

FLP.AzureFunctions is a .NET 8 Azure Functions project for managing bug tracking operations. It provides endpoints to create, read, update, and delete bugs, supports database migrations with Entity Framework Core, and includes comprehensive unit tests with code coverage reporting.

---

## Features

### Bug Operations

- **Create Bug**
  - **Endpoint:** `POST /api/Bug`
  - **Description:** Create a new bug by providing a title, description, and (optionally) an assigned user.
  - **Sample Request:**
  ```json
    {
        "title": "Sample Bug",
        "description": "Description of the bug.",
        "assignedToUserId": "GUID-OPTIONAL"
    }
    ```

- **Read Bugs**
  - **Get All:** `GET /api/Bug` (supports pagination and filtering by status)
  - **Get By ID:** `GET /api/Bug/{id}`

- **Update Bug**
  - **Endpoint:** `PUT /api/Bug` or `PATCH /api/Bug`
  - **Description:** Update an existing bug by providing its ID and the fields to update (title, description, status, assigned user).
  - **Sample Request:**
  ```json
    {
      "id": "BUG-GUID",
      "title": "Updated Title",
      "description": "Updated description.",
      "status": "InProgress",
      "assignedToUserId": "GUID-OPTIONAL"
    }
    ```

- **Delete Bug**
  - **Endpoint:** `DELETE /api/Bug/{id}`
  - **Description:** Delete a bug by its unique identifier.

#### BugStatus

The `status` field in bug operations represents the current state of a bug. The possible values are:

| Value       | Description    |
|-------------|----------------|
| `Open`      | The bug is newly created and not yet being worked on. |
| `InProgress`| Work on the bug has started. |
| `Resolved`  | The bug has been fixed but not yet closed. |
| `Closed`    | The bug is closed and no further action is required. |

Use these values when creating or updating a bug to indicate its current workflow state.

---

## Database Migration

Database schema changes are managed using Entity Framework Core migrations.

- **Create a Migration:**

```sh
    Add-Migration -Name <MigrationName> -OutputDir Data/Migrations -StartupProject FLP.AzureFunctions -Project FLP.Infra -Context AppDbContext
```

- **Apply Migrations:**

```sh
    Update-Database -StartupProject FLP.AzureFunctions -Project FLP.Infra -Context AppDbContext
```

Migration scripts are located in `FLP.Infra/Data/Migrations`.

---

## Unit Testing

Unit tests are written using xUnit and Moq.  
They cover all core bug operations (create, read, update, delete) and use mocks for repository and unit of work patterns.

- **View Code Coverage Report:**  
  [https://flpinheiro.github.io/FLP.AzureFunctions/index.html](https://flpinheiro.github.io/FLP.AzureFunctions/index.html)

---

## Requirements

- .NET 8 SDK
- Azure Functions Core Tools (for local development)
- SQL Server (or compatible database for EF Core)

---

## License

This project is licensed under the MIT License.

