# EDA - Event Driven Architecture
Para rodar o projeto execute o docker-compose.yaml
```sh
docker compose up --build -d
```
existe uma seed já configurada criando as tabelas e inserindo dados ficticios de Jhon Doe e Jane Doe, que já estão com os id's no client.http

apos do wallet core iniciar pode realizar algumas transações conforme o client.http.

aguardar o microsserviço de balance conectar no kafka e realizar o request ao endpoint de balances/{account_id}
