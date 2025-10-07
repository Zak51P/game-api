[README.md](https://github.com/user-attachments/files/22743061/README.md)

---

# Game API

Minimal **ASP.NET Core 8** project showcasing **Minimal APIs**, **Swagger/OpenAPI**, and **EF Core (SQLite)** for a simple games catalog.

> API docs: `http://localhost:5254/swagger`

---


## Overview

This repository contains a small backend service built with ASP.NET Core **Minimal APIs**. It exposes CRUD endpoints for a games catalog. The current demo uses an in-memory list (for quick start), while EF Core + SQLite are wired and ready for persistence.

---

## Features

* Minimal, function-style HTTP endpoints under `/games`
* **Swagger UI** for interactive API documentation
* **EF Core + SQLite** configured for local development
* Clear, extendable project structure

---

## Tech Stack

* **.NET 8**, **C#**
* **ASP.NET Core Minimal APIs**
* **EF Core** + **SQLite**
* **Swashbuckle** (Swagger/OpenAPI)

---

## Requirements

* .NET SDK 8.x

---

## Getting Started

```bash
git clone https://github.com/Zak51P/game-api.git
cd game-api

dotnet restore
dotnet run
```

Open **[http://localhost:5254/swagger](http://localhost:5254/swagger)** to browse and test the API.

> Optional: redirect `/` to Swagger by adding before `app.Run();`:
>
> ```csharp
> app.MapGet("/", () => Results.Redirect("/swagger"));
> ```

---

## Configuration

Connection string is read from `appsettings.json` if present, or defaults to SQLite:

```
Data Source=Game.db
```

* `Game.db` is **git-ignored** and used only for local development.
* On first run, tables are created automatically for demo purposes.

---

## API Reference

* **Swagger UI:** `http://localhost:5254/swagger`
* Base URL: `http://localhost:5254`

### Endpoints

* `GET    /games`
* `GET    /games/{id}`
* `POST   /games`
* `PUT    /games/{id}`
* `DELETE /games/{id}`

---

## Examples

### cURL

```bash
# List all
curl http://localhost:5254/games

# Create
curl -X POST http://localhost:5254/games \
  -H "Content-Type: application/json" \
  -d '{ "name":"Doom", "genre":"Shooter", "price":19.99, "releaseDate":"1993-12-10" }'

# Get by id
curl http://localhost:5254/games/1

# Update
curl -X PUT http://localhost:5254/games/1 \
  -H "Content-Type: application/json" \
  -d '{ "name":"Doom (1993)", "genre":"Shooter", "price":19.99, "releaseDate":"1993-12-10" }'

# Delete
curl -X DELETE http://localhost:5254/games/1
```

### VS Code REST Client

See `Requests/games.http` for ready-to-run requests.

---

## Project Structure

```
Game.api/
├─ Data/
│  └─ GameStoreContext.cs          # EF Core DbContext (SQLite)
├─ Dtos/
│  ├─ CreateGameDTO.cs
│  ├─ UpdateGameDTO.cs
│  └─ GameDTO.cs
├─ Entities/
│  ├─ Game.cs
│  └─ Genre.cs
├─ Endpoints/
│  └─ GamesEndpoints.cs            # /games endpoints (in-memory demo)
├─ Program.cs                      # App setup (DB, Swagger, endpoints)
└─ Requests/
   └─ games.http                   # HTTP request examples
```

---

## Roadmap

* Validation with `FluentValidation` + consistent error responses
* Persist `/games` to SQLite using EF Core (replace in-memory list)
* Integration tests via `WebApplicationFactory`
* GitHub Actions: `build` and `test`
* Dockerfile + docker-compose

---

## Contributing

Issues and PRs are welcome. For larger changes, please open an issue first to discuss what you would like to change.

---
