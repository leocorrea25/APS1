# Criar Produto

```bash
curl -X 'POST' \
  'http://localhost:5035/api/Product' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {seu_token}' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "glock 18",
  "description": "arma boa",
  "price": 29.90,
  "quantity": 5
}'
```
