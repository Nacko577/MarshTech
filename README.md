# Device Management System

A web application for tracking company-owned mobile devices — what they are, where they are, and who is using them.

## Tech Stack

- **Backend:** C#, ASP.NET Core Web API (.NET 10)
- **ORM:** Entity Framework Core 9
- **Database:** MS SQL Server
- **Frontend:** Angular (Phase 2)

## Prerequisites

- .NET 8 SDK or later
- SQL Server (Express edition is fine)
- SQL Server Management Studio (SSMS)

## Setup

### 1. Database

Open SSMS and run the scripts in order:

1. `db/01_CreateDatabase.sql` — creates the database and tables
2. `db/02_SeedData.sql` — inserts some dummy data

Both scripts are safe to run multiple times.

### 2. Connection string

Update the connection string in `api/appsettings.json` to point to your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=DeviceManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Run the API

```bash
cd api
dotnet run
```

Swagger UI will open automatically at `http://localhost:5274/swagger`.

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/devices` | Get all devices |
| GET | `/api/devices/{id}` | Get a device by id |
| POST | `/api/devices` | Create a device |
| PUT | `/api/devices/{id}` | Update a device |
| DELETE | `/api/devices/{id}` | Delete a device |
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get a user by id |
| POST | `/api/users` | Create a user |
| PUT | `/api/users/{id}` | Update a user |
| DELETE | `/api/users/{id}` | Delete a user |
