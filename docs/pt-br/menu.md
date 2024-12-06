# Documentação Completa

## Por onde começar

Crie um usuario para poder fazer login e obter um token JWT para acessar os endpoints protegidos.

## Pedido

| Método | Rota                                                                | Descrição                                  |
| ------ | ------------------------------------------------------------------- | ------------------------------------------ |
| POST   | [&nearr; /api/order](./order/create.md)                             | Cria um novo pedido                        |
| GET    | [&nearr; /api/order](./order/getAll.md)                             | Retorna todos os pedidos                   |
| GET    | [&nearr; /api/order/user-orders](./order/getByUser.md)              | Retorna todos os pedidos do usuario logado |
| PUT    | [&nearr; /api/order/{orderId}](./order/update.md)                   | Atualiza um pedido pelo ID                 |
| DELETE | [&nearr; /api/order/{orderId}](./order/delete.md)                   | Apaga um pedido                            |
| PATCH  | [&nearr; /api/order/{orderId}/complete](./order/markAsCompleted.md) | Marca o pedido como completo pelo ID       |

## Produto

| Método | Rota                                                         | Descrição                                      |
| ------ | ------------------------------------------------------------ | ---------------------------------------------- |
| POST   | [&nearr; /api/product](./product/create.md)                  | Cria um novo produto                           |
| GET    | [&nearr; /api/product](./product/getAll.md)                  | Retorna todos os produtos                      |
| DELETE | [&nearr; /api/product/{productId}](./product/delete.md)      | Apaga um produto                               |
| GET    | [&nearr; /api/product/{productId}](./product/getById.md)     | Retorna um produto pelo ID                     |
| PUT    | [&nearr; /api/product/{productId}](./product/update.md)      | Atualiza um produto pelo ID                    |
| GET    | [&nearr; /api/product/user-products](./product/getOrders.md) | Retorna todos os pedidos de um usuário pelo ID |

## Usuário

| Método | Rota                                            | Descrição                   |
| ------ | ----------------------------------------------- | --------------------------- |
| POST   | [&nearr; /api/user/login](./user/login.md)      | Faz login de um usuário     |
| POST   | [&nearr; /api/user](./user/create.md)           | Cria um novo usuário        |
| GET    | [&nearr; /api/user](./user/getAll.md)           | Retorna todos os usuários   |
| PUT    | [&nearr; /api/user/{userId}](./user/update.md)  | Atualiza um usuário pelo ID |
| DELETE | [&nearr; /api/user/{userId}](./user/delete.md)  | Apaga um usuário pelo ID    |
| GET    | [&nearr; /api/user/{userId}](./user/getById.md) | Retorna um usuário pelo ID  |
