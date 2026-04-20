# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build webapi-demo.slnx --configuration Release

# Run locally
dotnet run --project webapi-demo.csproj

# Run all tests
dotnet test webapi-demo.slnx

# Run a single test (by name filter)
dotnet test webapi-demo.slnx --filter "FullyQualifiedName~TestMethodName"

# Docker
docker compose up --build
```

## Architecture

Layered ASP.NET Core 10 Web API with SQLite via Entity Framework Core.

```
Controllers/    → HTTP layer (TodoController)
Services/       → Business logic (ToDoService implements IToDoService)
Data/           → EF Core DbContext (ToDoDbContext)
Models/         → Domain entity (ToDoItem)
DTO/            → Request/response contracts (ToDoItemDTO)
Interfaces/     → Service abstractions
CustomMiddleware/→ Request/response logging
Migrations/     → EF Core-generated, do not edit manually
```

**Key patterns:**
- Controllers map between DTOs and domain entities; all DB access goes through the service layer
- All service and controller methods are async
- JSON Patch (`PATCH /api/todo/{id}`) uses Newtonsoft.Json; the rest of the app uses System.Text.Json
- OpenAPI UI served via Scalar at `/scalar/v1`

**Testing:** xUnit 3 with Coverlet for coverage. Tests live in `tests/webapi-demo.Tests/`. Code coverage config is in `coverlet.runsettings`.

**Code style:** Enforced via `.editorconfig` — file-scoped namespaces, 4-space indent, UTF-8.
