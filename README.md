# NET 6 Web API Role-Based Authorization with JSON Web Tokens (JWT)


## General architecture
<img src="/pictures/architecture.png" title="architecture"  width="900">


## Auction MicroService

### Nuget Packages
```
Automapper.Extensions.Microsoft.DependencyInjection
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Npgsql.EntityFrameworkCore.PostGreSQL
MassTransit.RabbitMQ
MassTransit.EntityFrameworkCore
```

### PostgreSQL

- run
```
Add-Migration InitialCreate
```

- Add PostgreSQL container
```
docker compose down
docker compose up -d
```
<img src="/pictures/postgres.png" title="postgres"  width="900">
