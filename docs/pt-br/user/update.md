# Atualizar Usuário

Atualiza um usuário pelo ID.

> [!TIP]
> O que estiver `null` não será atualizado.

```bash
curl -X 'PUT' \
  'http://localhost:5035/api/User/4' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {seu_token}' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 4,
  "name": "chato da silva",
  "email": "chato@gmail.com",
  "password": "0912837",
  "phoneNumber": null,
  "isSeller": true,
  "address": null
}'
```
