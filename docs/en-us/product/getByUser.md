# Buscar Produos pelo Usuário Logado

Retorna todos os produtos de um usuário logado.

```bash
curl -X 'GET' \
  'http://localhost:5035/api/Product/user-products' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {seu_token}'
```