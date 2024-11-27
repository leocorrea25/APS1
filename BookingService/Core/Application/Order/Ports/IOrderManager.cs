using Application.Order.Request;
using Application.Order.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Ports
{
    public interface IOrderManager
    {
        Task<OrderResponse> CreateOrder(Domain.Order.Entities.Order request);
        Task<OrderResponse> GetOrder(int orderId);
        Task<IEnumerable<Domain.Order.Entities.Order>> GetAllOrders();
        Task<OrderResponse> UpdateOrder(Domain.Order.Entities.Order request);
        Task<OrderResponse> DeleteOrder(int orderId);
        Task MarkAsCompleted(int orderId);
    }
}
