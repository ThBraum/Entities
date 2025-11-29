# Entidades (ASP.NET Core + PostgreSQL + Docker Compose)

Este projeto contém um exemplo mínimo com camadas Repository/Service/DTOs, controllers API e suporte a PostgreSQL.

Como executar com Docker Compose:

1. Build e subir os serviços (irá construir a imagem do `web` e iniciar `db`):

```bash
docker compose up --build
```

2. A aplicação web ficará disponível em `http://localhost:5001` (mapeamento `5001:80`).

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

Notas importantes:
- **Variáveis de ambiente**: o `SampleDbConnection` é injetado via `docker-compose.yml` e aponta para o host `db` (nome do serviço Postgres no Compose). O `web` também define `DB_HOST` e `DB_PORT` usados pelo script de inicialização.
- **Espera do DB**: o contêiner `web` inclui um script `wait-for-db.sh` que aguarda o Postgres estar aceitando conexões antes de iniciar a aplicação. Isso evita que a aplicação tente aplicar migrações antes do banco estar pronto.
- **Migrações**: o app tentará aplicar migrações automaticamente em startup (`db.Database.Migrate()`); em caso de falha o processo pode usar `EnsureCreated()` como fallback. Para forçar aplicação manual de migrações (opcional):

```bash
# executar a partir do host, usando a cadeia de conexão do docker-compose
dotnet ef database update --connection "Host=localhost;Port=5432;Database=SampleDb;Username=dev_user;Password=dev_password"
```

- **Reset do banco (dev)**: para recriar um banco limpo e reaplicar tudo do zero (apaga dados), rode:

```bash
docker compose down -v && docker compose up --build
```

- **Conectar localmente**: se preferir usar um Postgres local, ajuste `SampleDbConnection` em `appsettings.json` ou passe a variável de ambiente apropriada ao rodar o container.

