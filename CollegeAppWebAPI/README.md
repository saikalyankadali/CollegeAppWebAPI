# CollegeAppWebAPI

Backend service for CollegeApp — an ASP.NET Core Web API for managing students, users, roles, and role privileges. Secured with JWT authentication and built on Entity Framework Core with a generic repository pattern.

## Tech Stack

- **.NET** — ASP.NET Core Web API
- **ORM** — Entity Framework Core
- **Database** — SQL Server (CollegeDB)
- **Security** — JWT Bearer authentication, role-based authorization
- **Password hashing** — PBKDF2 (HMAC-SHA256) with a per-user salt
- **Mapping** — AutoMapper
- **Patterns** — Generic repository (`ICollegeRepository<T>`) + service layer
- **Logging** — log4net

## Project Structure

```
CollegeAppWebAPI/
├── Configurations/   # AutoMapper profiles and entity configurations
├── Controllers/      # API controllers (Login, Student, User, Role, RolePrivilege, UserType)
├── Data/             # DbContext, entities, generic repository
├── Logger/           # Logging setup
├── Migrations/       # EF Core migrations
├── Models/           # DTOs and the APIResponse envelope
├── Services/         # Business logic (e.g. UserService)
├── Validators/       # Custom validation
├── Program.cs        # Application entry point and DI setup
└── appsettings.json  # Configuration (connection string, JWT secret)
```

## API Response Format

All endpoints return a consistent envelope:

```json
{
  "status": true,
  "statusCode": 200,
  "data": { },
  "errors": []
}
```

- `status` — logical success or failure of the operation
- `statusCode` — the HTTP status represented by the result
- `data` — the payload (object, list, or value)
- `errors` — error messages when `status` is `false`

Note: some endpoints return business failures (such as invalid credentials) with an HTTP 200 status and `status: false`. Clients should check the `status` field rather than relying solely on HTTP error codes.

## Authentication & Authorization

Obtain a token from the login endpoint, then send it as a Bearer token on protected requests:

```
Authorization: Bearer <token>
```

Login is validated against the **User table**. Passwords are stored as a PBKDF2 hash with a per-user salt; at login, the entered password is re-hashed with the stored salt and compared. The user's role is resolved through the user–role mapping and written into the token as a role claim, which protected endpoints authorize against.

## Data Model (high level)

- **Student** — student records (linked to Department)
- **Department** — departments
- **User** — application users (hashed password, salt, soft delete, audit dates)
- **Role** — roles
- **RolePrivilege** — privileges belonging to a role
- **UserRoleMapping** — links users to roles
- **UserType** — user type reference data

## API Endpoints

### Login

| Method | Route        | Description                              | Auth      |
| ------ | ------------ | ---------------------------------------- | --------- |
| POST   | `/api/Login` | Authenticate against the User table      | Anonymous |

### Student

| Method | Route                                    | Description                | Auth      |
| ------ | ---------------------------------------- | -------------------------- | --------- |
| GET    | `/api/Student/All`                       | Get all students           | Admin     |
| GET    | `/api/Student/Get/{id}`                  | Get a student by id        | Anonymous |
| POST   | `/api/Student/createstudent`             | Create a student           | Admin     |
| PUT    | `/api/Student/updaterecord`              | Update a student record    | Admin     |
| PATCH  | `/api/Student/updaterecordpartial/{id}`  | Partially update a student | Admin     |
| DELETE | `/api/Student/deletestudent/{id}`        | Delete a student by id     | Admin     |
| GET    | `/api/Student/LogMessage`                | Logging test endpoint      | Admin     |

### User

| Method | Route                  | Description           |
| ------ | ---------------------- | --------------------- |
| POST   | `/api/User/Create`     | Create a user         |
| GET    | `/api/User/All`        | Get all users         |
| GET    | `/api/User/{id}`       | Get a user by id      |
| GET    | `/api/User/{username}` | Get a user by username|
| PUT    | `/api/User/Update`     | Update a user         |
| DELETE | `/api/User/Delete/{id}`| Soft delete a user    |

### Role

| Method | Route                                  | Description                |
| ------ | -------------------------------------- | -------------------------- |
| POST   | `/api/Role/Create`                     | Create a role              |
| GET    | `/api/Role/All`                        | Get all roles              |
| GET    | `/api/Role/{id}`                       | Get a role by id           |
| PUT    | `/api/Role/updaterecord`               | Update a role              |
| PATCH  | `/api/Role/updaterecordpartial/{id}`   | Partially update a role    |
| DELETE | `/api/Role/deleterole/{id}`            | Delete a role by id        |

### Role Privilege

| Method | Route                                                  | Description                       |
| ------ | ------------------------------------------------------ | --------------------------------- |
| POST   | `/api/RolePrivilegePrivilege/Create`                   | Add a privilege to a role         |
| GET    | `/api/RolePrivilegePrivilege/All`                      | Get all role privileges           |
| GET    | `/api/RolePrivilegePrivilege/{id}`                     | Get a role privilege by id        |
| GET    | `/api/RolePrivilegePrivilege/ByRole/{roleId}`          | Get privileges for a role         |
| PUT    | `/api/RolePrivilegePrivilege/updaterecord`             | Update a role privilege           |
| PATCH  | `/api/RolePrivilegePrivilege/updaterecordpartial/{id}` | Partially update a role privilege |
| DELETE | `/api/RolePrivilegePrivilege/deleterolePrivilege/{id}` | Remove a privilege from a role    |

### User Type

| Method | Route                | Description          |
| ------ | -------------------- | -------------------- |
| GET    | `/api/UserType/All`  | Get all user types   |

> Confirm the User Type routes against your controller; the client expects `GET /api/UserType/All`.

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server
- A tool to run requests (Swagger, Postman, or the included `.http` file)

### Configuration

Update `appsettings.json` with your connection string and JWT secret:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JWTSecret": "your-strong-secret-key"
}
```

### Database

Apply the EF Core migrations to create the database:

```bash
dotnet ef database update
```

### Seeding a login user

Because passwords are PBKDF2-hashed with a salt, create your first user through `POST /api/User/Create` (which generates the hash and salt) rather than inserting plaintext directly in SQL. Map that user to a role via the user–role mapping so the login token carries the correct role claim.

### Run

```bash
dotnet run
```

The API starts on the URL shown in the console (e.g. `https://localhost:7xxx`). Swagger UI is available at `/swagger` in development.

## CORS

To allow the Angular client to call the API during development, ensure the client origin (`http://localhost:4200`) is permitted and that `UseCors` is registered before `UseAuthentication` and `UseAuthorization` in `Program.cs`.

## Purpose

Built for learning ASP.NET Core Web API, JWT security, Entity Framework Core, the repository pattern, AutoMapper, and secure password hashing.
