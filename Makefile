.PHONY: start stop logs db_psql migrate

COMPOSE ?= docker compose
WEB ?= web
DB ?= db
DB_USER ?= dev_user
DB_NAME ?= SampleDb
DB_PORT ?= 5432

start:
	$(COMPOSE) up --build

stop:
	$(COMPOSE) down

logs:
	$(COMPOSE) logs -f $(WEB)

db_psql:
	$(COMPOSE) exec $(DB) psql -U $(DB_USER) -d $(DB_NAME)

migrate:
	@echo "Tentando aplicar migrações dentro do container '$(WEB)'..."
	@$(COMPOSE) exec $(WEB) dotnet ef database update --connection 'Host=$(DB);Port=$(DB_PORT);Database=$(DB_NAME);Username=$(DB_USER);Password=dev_password' || \
	(echo "Fallback: executando dotnet ef no host (requer .NET SDK instalado)"; dotnet ef database update --connection "Host=localhost;Port=$(DB_PORT);Database=$(DB_NAME);Username=$(DB_USER);Password=dev_password")
