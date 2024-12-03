using Application.Dtos;
using Application.Order.Ports;
using Application.Order.Request;
using Application.Order.Responses;
using Application.Product.Ports;
using Domain.Order.Entities;
using Domain.Order.Ports;

namespace Application.Order
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository; 
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;

        public OrderManager(IOrderRepository orderRepository, IUserRepository userRepository, IAddressRepository addressRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _productRepository = productRepository;
        }

        public async Task<Domain.Order.Entities.Order> CreateOrder(OrderRequest request)
        {
            try
            {
                // Verifique se o usuário é um comprador e não um vendedor
                var user = await _userRepository.GetUser(request.UserId);
                if (user == null || user.IsSeller)
                {
                    throw new Exception("Usuario não pode comprar o produto, pois é um vendedor");
                }

                // Se a opção de entrega for "retirada", o endereço não é necessário
                Domain.Order.Entities.Address? address = null;
                int? addressId = null;
                if (request.DeliveryOption.Equals("entrega", StringComparison.OrdinalIgnoreCase))
                {
                    addressId = user.AddressId;
                    address = await _addressRepository.GetAddress(user.AddressId);
                }

                // Obtenha o produto e verifique a quantidade disponível
                var product = await _productRepository.GetProduct(request.ProductId);
                if (product == null || product.Quantity < request.ProductQuantity)
                {
                    throw new Exception("The product is out of stock or does not have enough quantity");
                }

                // Atualize a quantidade do produto
                product.Quantity -= request.ProductQuantity;
                await _productRepository.UpdateProduct(product);

                // Crie a entidade Order a partir do OrderRequest
                var order = new Domain.Order.Entities.Order
                {
                    DeliveryOption = request.DeliveryOption,
                    AddressId = addressId,
                    Address = address,
                    AdditionalInstructions = request.AdditionalInstructions,
                    ProductId = request.ProductId,
                    ProductQuantity = request.ProductQuantity,
                    UserId = request.UserId,
                    User = user
                };

                // Crie a ordem no repositório
                var orderId = await _orderRepository.Create(order);

                // Obtenha a ordem criada
                var createdOrder = await _orderRepository.Get(orderId);

                return createdOrder;
            }
            catch (Exception ex)
            {
                throw new Exception("The order is not valid: " + ex.Message);
            }
        }

        public async Task<bool> MarkAsCompleted(int orderId, int userId)
        {
            var order = await _orderRepository.Get(orderId);
            var user = await _userRepository.GetUser(userId);

            if(user.IsSeller != true)
            {
                throw new Exception("Usuário não pode marcar o pedido como concluído, pois não é um vendedor");
            }

            if (order == null)
            {
                return false;
            }

            order.MarkAsCompleted();

            await _orderRepository.Update(order);
            return true;
        }

        public async Task<OrderResponse> GetOrder(int orderId)
        {
            var order = await _orderRepository.Get(orderId);

            if (order == null)
            {
                return new OrderResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No order record was found with the given id"
                };
            }

            return new OrderResponse
            {
                Data = OrderDto.MapToDto(order),
                Success = true,
            };
        }

        public async Task<IEnumerable<Domain.Order.Entities.Order>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAll();

            return orders;
        }

        public async Task<OrderResponse> UpdateOrder(EditOrderRequest order)
        {
            try
            {
                var existingOrder = await _orderRepository.Get(order.OrderId);

                if (existingOrder == null)
                {
                    return new OrderResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No order record was found with the given id"
                    };
                }

                // Atualize as propriedades do pedido diretamente
                existingOrder.DeliveryOption = order.DeliveryOption;
                existingOrder.AdditionalInstructions = order.AdditionalInstructions;
                existingOrder.ProductId = order.ProductId;
                existingOrder.ProductQuantity = order.ProductQuantity;
                existingOrder.UserId = order.UserId;
                // Atualize outras propriedades conforme necessário

                await _orderRepository.Update(existingOrder);

                return new OrderResponse
                {
                    Data = OrderDto.MapToDto(existingOrder),
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<OrderResponse> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _orderRepository.Get(orderId);

                if (order == null)
                {
                    return new OrderResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No order record was found with the given id"
                    };
                }

                await _orderRepository.Delete(orderId);

                return new OrderResponse
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when deleting from DB"
                };
            }
        }

        public async Task<IEnumerable<Domain.Order.Entities.Order>> GetOrdertByUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                return Enumerable.Empty<Domain.Order.Entities.Order>();
            }

            var orders = await _orderRepository.GetAll();
            if (orders == null)
            {
                return Enumerable.Empty<Domain.Order.Entities.Order>();
            }

            if (user.IsSeller)
            {
                // Se o usuário for um vendedor, buscar ordens pelos IDs dos produtos que ele criou
                var products = await _productRepository.GetAllProducts();
                if (products == null)
                {
                    return Enumerable.Empty<Domain.Order.Entities.Order>();
                }

                var productIds = products.Where(p => p.UserId == userId).Select(p => p.Id);
                var sellerOrders = orders.Where(o => productIds.Contains(o.ProductId));
                return sellerOrders;
            }
            else
            {
                // Se o usuário for um comprador, buscar ordens pelo ID do usuário
                var userOrders = orders.Where(o => o.UserId == userId);
                return userOrders;
            }
        }
    }
}
