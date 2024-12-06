# Create Order

Create a new order.

> [!IMPORTANT]
>
> - The `deliveryOption` field must be either "entrega" or "retirada".
> - The `userId` field must be of a User that `isSeller` is `false`.

```bash
curl -X 'POST' \
  'http://localhost:5035/api/Order' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer {seu_token}' \
  -H 'Content-Type: application/json' \
  -d '{
  "deliveryOption": "entrega",
  "additionalInstructions": "cuidado com o cao bravo",
  "productId": 1,
  "productQuantity": 1,
  "userId": 7
}'
```
