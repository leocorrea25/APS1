# Create User 

Create a new user.

```bash
curl -X 'POST' \
  'http://localhost:5035/api/User' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "joao",
  "email": "joao@gmail.com",
  "password": "123456",
  "phoneNumber": "+5541988776655",
  "isSeller": true,
  "postalCodeAddress": 123,
  "numberAddress": 123,
  "street": "Rua legal",
  "city": "cidade ruim"
}'
```
