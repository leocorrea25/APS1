using Application.Order.Request;
using Application.Order.Responses;
using Domain.Order.Entities;

namespace Application.Order.Ports
{
    public interface IOrderManager
    {
        Task<Domain.Order.Entities.Order> CreateOrder(OrderRequest request);
        Task<OrderResponse> GetOrder(int orderId);
        Task<IEnumerable<Domain.Order.Entities.Order>> GetAllOrders();
        Task<OrderResponse> UpdateOrder(EditOrderRequest request);
        Task<IEnumerable<Domain.Order.Entities.Order>> GetOrdertByUser(int userId);
        Task<OrderResponse> DeleteOrder(int orderId);
        Task<bool> MarkAsCompleted(int orderId, int userId);
    }
}
