# Atualizar Produto

```bash
curl -X 'PUT' \
  'http://localhost:5035/api/Product/1' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {seu_token}' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "faca ak47",
  "description": "boa mesmo hein",
  "price": 89.99,
  "quantity": 132
}'
```