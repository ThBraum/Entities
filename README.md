# Entidades (ASP.NET Core + PostgreSQL + Docker Compose)

Este projeto contém um exemplo mínimo com camadas Repository/Service/DTOs, controllers API e suporte a PostgreSQL.

Como executar com Docker Compose:

1. Build e subir os serviços (irá construir a imagem do `web` e iniciar `db`):

```bash
docker compose up --build
```

2. A aplicação web ficará disponível em `http://localhost:5000`.

- Endpoints API principais:
  - `GET /api/teams`
  - `GET /api/teams/{id}`
  - `POST /api/teams`
  - `PUT /api/teams/{id}`
  - `DELETE /api/teams/{id}`

  - `GET /api/drivers`
  - `GET /api/drivers/{id}`
  - `POST /api/drivers`
  - `PUT /api/drivers/{id}`
  - `DELETE /api/drivers/{id}`

Notas:
- O `SampleDbConnection` é injetado via variável de ambiente no `docker-compose.yml` e aponta para o host `db` (nome do serviço Postgres no Compose).
- O app aplica migrações automaticamente ao iniciar (chamada `db.Database.Migrate()` em `Program.cs`).
- Se quiser conectar a um PostgreSQL local em vez do Compose, atualize `appsettings.json` ou forneça a variável de ambiente `SampleDbConnection`.
