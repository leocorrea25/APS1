# Criar Usuário

Cria um novo usuário.

```bash
curl -X POST https://localhost:5035/api/user \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "joao",
  "email": "joao@gmail.com",
  "password": "123456",
  "phoneNumber": "+5541988776655",
  "isSeller": true,
  "postalCodeAddress": 81810630,
  "numberAddress": 123,
  "street": "Rua legal",
  "city": "cidade ruim"
}'
```
