# 🥤 Domča – Hydration Tracker

[![.NET Core Build](https://github.com/musakai/Domca/actions/workflows/build.yaml/badge.svg)](https://github.com/musakai/Domca/actions/workflows/build.yaml)
![Made with Love](https://img.shields.io/badge/Made_with-Love-red)
![.NET](https://img.shields.io/badge/.NET-10-blueviolet)
![C#](https://img.shields.io/badge/C%23-14-239120)
![Blazor](https://img.shields.io/badge/Blazor-WASM-purple)
![EF Core](https://img.shields.io/badge/EF_Core-10.0-green)
![Docker](https://img.shields.io/badge/Docker-enabled-blue)
![Last Commit](https://img.shields.io/github/last-commit/musakai/Domca)
![Issues](https://img.shields.io/github/issues/musakai/Domca)
![Top Language](https://img.shields.io/github/languages/top/musakai/Domca)

A lightweight Hydration tracking app built with **Blazor WebAssembly** and **ASP.NET Core Web API**, designed for exactly two users.  
Created as a personal wellness tool to monitor daily water intake with clarity and simplicity.

---

## 🚀 Tech Stack

- **.NET 10** – unified platform for backend and frontend
- **Blazor WebAssembly** – client-side UI running in the browser
- **ASP.NET Core Web API** – RESTful backend
- **Entity Framework Core** – ORM for data access
- **Docker** – containerized deployment

---

## 📁 Solution Structure

The solution consists of five modular projects:

| Project                     | Purpose                                      |
|-----------------------------|----------------------------------------------|
| `Domca.API`                 | Web API                                      |
| `Domca.Blazor`              | Blazor WebAssembly frontend                  |
| `Domca.Core`                | Domain models, DTOs, interfaces, helpers     |
| `Domca.EntityFrameworkCore` | EF Core setup and migrations                 |
| `Domca.Repositories`        | Repository implementations                   |
| `Domca.Tests`               | Unit tests and test data context             |

---

## ⚙️ Running the App

### Locally

1. Install [.NET 10 SDK](https://dotnet.microsoft.com).
2. Restore and build the solution:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/Domea.API
3. The Blazor frontend will launch with the API.

### With Docker
1. Make sure Docker is installed.
2. Run: `docker-compose up --build`
3. Access the app at http://localhost:5000.

