# Login

```bash
curl -X 'POST' \
  'http://localhost:5035/api/User/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "email": "joao@gmail.com",
  "password": "123456"
}'
```