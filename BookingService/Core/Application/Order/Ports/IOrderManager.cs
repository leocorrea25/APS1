using Application.Order.Responses;
using Domain.Order.Entities;

namespace Application.Order.Ports
{
    public interface IOrderManager
    {
        Task<Domain.Order.Entities.Order> CreateOrder(OrderRequest request);
        Task<OrderResponse> GetOrder(int orderId);
        Task<IEnumerable<Domain.Order.Entities.Order>> GetAllOrders();
        Task<OrderResponse> UpdateOrder(Domain.Order.Entities.Order request);
        Task<OrderResponse> DeleteOrder(int orderId);
        Task<bool> MarkAsCompleted(int orderId, int userId);
    }
}
