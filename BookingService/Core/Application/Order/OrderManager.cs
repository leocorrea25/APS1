using Application.Dtos;
using Application.Order.Ports;
using Application.Order.Responses;
using Domain.Order.Ports;

namespace Application.Order
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> CreateOrder(Domain.Order.Entities.Order request)
        {
            try
            {

                var orderId = await _orderRepository.Create(request);

                var order = await _orderRepository.Get(orderId);

                return new OrderResponse
                {
                    Data = OrderDto.MapToDto(order),
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new OrderResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Message = "The order is not valid"
                };
            }
            
        }

        public async Task MarkAsCompleted(int orderId)
        {
            var order = await _orderRepository.Get(orderId);

            if (order == null)
            {
                return;
            }

            order.MarkAsCompleted();

            await _orderRepository.Update(order);
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

        public async Task<OrderResponse> UpdateOrder(Domain.Order.Entities.Order order)
        {
            try
            {
                var existingOrder = await _orderRepository.Get(order.Id);

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
                existingOrder = order;
                existingOrder.Quantity = order.Quantity;
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
    }
}
