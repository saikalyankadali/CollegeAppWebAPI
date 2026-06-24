# CollegeApp

A full-stack records management application built for learning and practice. The backend is an ASP.NET Core Web API secured with JWT authentication; the frontend is an Angular single-page application that consumes it. It covers student records along with role-based user administration.

## Overview

CollegeApp lets an authenticated administrator manage students, users, roles, and role privileges through a clean web interface backed by a secured REST API. Users authenticate against the database; passwords are hashed with PBKDF2 and a per-user salt.

| Layer    | Technology                                              |
| -------- | ------------------------------------------------------- |
| Backend  | ASP.NET Core Web API, Entity Framework Core, SQL Server |
| Security | JWT Bearer authentication, role-based authorization     |
| Frontend | Angular (standalone components), TypeScript             |
| Database | SQL Server (CollegeDB)                                  |

## Repository Structure

```
CollegeApp/
├── CollegeAppWebAPI/           # ASP.NET Core Web API (backend)
│   └── README.md
├── CollegeAppClient/           # Angular SPA (frontend)
│   └── README.md
└── README.md                   # You are here
```

## Architecture

```
┌──────────────────┐    HTTPS / JWT     ┌──────────────────┐      EF Core      ┌──────────────┐
│ CollegeAppClient │ ─────────────────> │ CollegeAppWebAPI │ ────────────────> │  SQL Server  │
│  (Angular SPA)   │                    │   (REST API)     │                   │  (CollegeDB) │
│                  │ <───────────────── │                  │ <──────────────── │              │
└──────────────────┘   APIResponse JSON └──────────────────┘     EF entities   └──────────────┘
```

The client authenticates against the API's `/api/Login` endpoint, stores the returned JWT,
and attaches it as a `Bearer` token on every subsequent request. The API validates the token,
enforces role-based access, and persists data to SQL Server through Entity Framework Core.

## Modules

- **Students** — create, read, update, and delete student records
- **Users** — create and manage application users (PBKDF2-hashed passwords, soft delete)
- **Roles** — manage roles
- **Role Privileges** — assign and remove privileges per role
- **User Types** — reference data used when creating users

## Getting Started

Each project has its own setup instructions. Start with the backend, then the frontend.

1. **Backend** — see [`CollegeAppWebAPI/README.md`](./CollegeAppWebAPI/README.md)
2. **Frontend** — see [`CollegeAppClient/README.md`](./CollegeAppClient/README.md)

Log in with a user that exists in the database. The user's role (resolved through the user–role mapping) determines access to protected endpoints.

## Features

- JWT-based authentication with role-based authorization
- Login validated against the User table with PBKDF2 + per-user salt password hashing
- CRUD for students, users, roles, role privileges, and user types
- Consistent `APIResponse` envelope across all endpoints
- Generic repository pattern and AutoMapper on the backend
- Soft delete for users
- Standalone-component Angular frontend with route guards and an HTTP interceptor

## Purpose

This project is built for hands-on learning and practice with ASP.NET Core Web API, JWT security, Entity Framework Core, the repository pattern, and Angular integration. It is not intended for production use.

## License

This project is for educational purposes.
