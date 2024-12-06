# Criar Produto

```bash
curl -X 'POST' \
  'http://localhost:5035/api/Product' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyIiwiZW1haWwiOiJhQGEiLCJyb2xlIjoiU2VsbGVyIiwibmJmIjoxNzMzNTEzNzQyLCJleHAiOjE3MzM1MTczNDIsImlhdCI6MTczMzUxMzc0Mn0.-HVkG0pANkN7M4BDBTE5P5Ejq573kjf9bqW6pL3QBa8' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "name": "glock 18",
  "description": "arma boa",
  "price": 29.90,
  "quantity": 5
}'
```
