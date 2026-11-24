<div align="center">

# 🍽️ ComandasApp — Real-Time Restaurant Order Management System

**Plataforma Full-Stack en tiempo real para digitalizar la toma de pedidos en mesa, flujo de cocina y facturación en el sector gastronómico**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?style=flat-square&logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![GraphQL](https://img.shields.io/badge/GraphQL-HotChocolate_14-E10098?style=flat-square&logo=graphql&logoColor=white)](https://chillicream.com/docs/hotchocolate)
[![SignalR](https://img.shields.io/badge/Realtime-SignalR-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/signalr)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-EF_Core_10-4169E1?style=flat-square&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker&logoColor=white)](https://www.docker.com/)

</div>

---

## 📖 Descripción General

**ComandasApp** nace de una necesidad operativa real en el sector gastronómico: eliminar los cuellos de botella, pérdidas de comandas en papel y demoras entre la toma del pedido en mesa y la preparación en cocina. Actualmente implementada en **fase Beta en un restaurante real**, la plataforma sincroniza en tiempo real los estados de cada orden entre meseros, cocina y caja.

---

## 🏗️ Arquitectura & Stack Tecnológico

El sistema está diseñado bajo **Clean Architecture y Domain-Driven Design (DDD)**, separando estrictamente las reglas de negocio de la infraestructura y la interfaz de usuario:

```text
ComandasApp.slnx
├── ComandasApp.Domain/          # Entidades de dominio, agregados, invariantes y contratos base
├── ComandasApp.Application/     # Casos de uso orquestados con CQRS (MediatR 12)
├── ComandasApp.Contracts/       # DTOs y contratos compartidos entre Backend y Cliente WASM
├── ComandasApp.Infrastructure/  # Persistencia con Entity Framework Core 10 y PostgreSQL (Npgsql)
├── ComandasApp/                 # Host ASP.NET Core 10, endpoint GraphQL (HotChocolate 14) y SignalR Hubs
├── ComandasApp.Client/          # SPA Frontend en Blazor WebAssembly + cliente SignalR en tiempo real
└── ComandasApp.Test/            # Pruebas unitarias de dominio y aplicación con xUnit y Coverlet
```

| Capa | Tecnologías |
| :--- | :--- |
| **Backend & API** | C#, .NET 10, ASP.NET Core, HotChocolate 14 (GraphQL), MediatR 12 (CQRS), SignalR |
| **Frontend** | Blazor WebAssembly (.NET 10), SignalR Client (WebSockets), Diseño Responsivo Mobile-First |
| **Base de Datos** | PostgreSQL, Entity Framework Core 10 (`Npgsql`) |
| **DevOps & QA** | Docker, Docker Compose, xUnit, Coverlet Code Coverage |

---

## 🚀 Ejecución Local con Docker

```bash
docker compose up -d
dotnet run --project ComandasApp/ComandasApp.csproj
```

---

## 👤 Autor

**Juan Fernando Portilla Collazos**  
Ingeniería de Sistemas — Universidad del Cauca  
[Portafolio Web](https://bootstrapportfolio-sable.vercel.app/) · [GitHub](https://github.com/Jfernando014) · [LinkedIn](https://www.linkedin.com/in/juan-fernando-portilla-collazos-86839a3a8)
