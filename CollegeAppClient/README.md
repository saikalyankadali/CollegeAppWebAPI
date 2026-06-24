# CollegeAppClient

Frontend for CollegeApp — an Angular single-page application for managing students, users, roles, and role privileges. It authenticates against the CollegeAppWebAPI, stores the JWT, and provides admin UIs over the secured REST API.

## Tech Stack

- **Framework** — Angular (standalone components)
- **Language** — TypeScript
- **HTTP** — Angular HttpClient with a functional interceptor
- **Routing** — Angular Router with route guards

## Features

- Login page with JWT authentication
- **Students** — list, add, edit, and delete student records
- **Users** — list and add users (with user type), soft delete
- **Roles** — list, add, edit, and delete roles
- **Role Privileges** — add and remove privileges per role
- Automatic Bearer-token attachment via an HTTP interceptor
- Route protection via an auth guard

## Project Structure

```
src/app/
├── components/
│   ├── login/                # Login page
│   ├── student-list/         # Students: list, create, edit, delete
│   ├── users/                # Users: list, create, delete
│   └── roles/                # Roles + per-role privileges
├── services/
│   ├── auth.service.ts       # Login, token storage, session state
│   ├── student.service.ts    # Student CRUD
│   ├── user.service.ts       # User CRUD
│   ├── user-type.service.ts  # User type lookup
│   ├── role.service.ts       # Role CRUD
│   └── role-privilege.service.ts  # Role privilege add/remove
├── interceptors/
│   └── auth.interceptor.ts   # Attaches the Bearer token
├── guards/
│   └── auth.guard.ts         # Protects authenticated routes
├── models/                   # Student, user, role, privilege, APIResponse interfaces
├── app.config.ts             # App providers (router, http, interceptor)
└── app.routes.ts             # Route definitions
```

## API Integration

The client consumes the CollegeAppWebAPI. All responses share a common envelope, and the payload is read from the `data` property:

```json
{
  "status": true,
  "statusCode": 200,
  "data": { },
  "errors": []
}
```

The API returns some business failures (such as invalid credentials) with an HTTP 200 status and `status: false`, so the client checks the `status` field rather than relying solely on HTTP error codes. Each service unwraps `data` on success and surfaces `errors` on logical failure.

## Getting Started

### Prerequisites

- Node.js and npm
- Angular CLI
- A running instance of CollegeAppWebAPI

### Install

```bash
npm install
```

### Configure the API URL

Set the backend base URL in `src/environments/environment.ts`:

```ts
export const environment = {
  apiUrl: 'https://localhost:7xxx/api'  // match your API port
};
```

### Run

```bash
ng serve
```

Open `http://localhost:4200`. Log in with a user that exists in the API's database.

## Notes

- Ensure the API has CORS configured to allow `http://localhost:4200`.
- The JWT is stored in the browser and attached to every request automatically by the interceptor.
- Access to protected pages depends on the role carried in the JWT, which is resolved from the user's role mapping on the backend.

## Purpose

Built for learning Angular standalone components, JWT-based authentication, HTTP interceptors, route guards, and integration with an ASP.NET Core Web API.
