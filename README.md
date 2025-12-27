# Restaurants Management System

Lightweight .NET backend for managing restaurants, menus, tables, reservations and orders. Built with Clean Architecture and a CQRS-friendly application layer.

## Highlights
- Layered solution: Domain, Application, Infrastructure, API
- Thin controllers → MediatR/handlers in Application
- Persistence in Infrastructure (EF Core expected)
- Unit/integration test projects included

## Quick start
Prereqs: .NET SDK, optional DB (SQL Server/Postgres)

Clone and run:
```bash
git clone https://github.com/moatazbadr/RestaurantsMangementSystem.git
cd RestaurantsMangementSystem
dotnet restore
dotnet build
cd Restaurants.API
dotnet run
```

Run tests:
```bash
dotnet test
```

## Typical API endpoints
- GET /api/restaurants
- GET /api/restaurants/{id}
- POST /api/restaurants
- PUT /api/restaurants/{id}
- POST /api/restaurants/{id}/reservations
- POST /api/restaurants/{id}/orders

Open Swagger at /swagger when the API is running to see exact routes.

## Contributing
Fork → branch → PR. Include tests for new behavior.

## License & Contact
Add a LICENSE file and update this section.  
Maintainer: moatazbadr — https://github.com/moatazbadr/RestaurantsMangementSystem