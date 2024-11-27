using Domain.Order.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public OrderRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Order.Entities.Order order)
        {
            await _hotelDbContext.Order.AddAsync(order);
            await _hotelDbContext.SaveChangesAsync();
            return order.Id;
        }

        public async Task Delete(int id)
        {
            var order = await _hotelDbContext.Order.FindAsync(id);
            if (order != null)
            {
                _hotelDbContext.Order.Remove(order);
                await _hotelDbContext.SaveChangesAsync();
            }
        }

        public async Task<Domain.Order.Entities.Order> Get(int id) => await _hotelDbContext.Order
                .Include(o => o.Address) // Inclui a propriedade de navegação Address
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Domain.Order.Entities.Order>> GetAll()
        {
            return await _hotelDbContext.Order.ToListAsync();
        }

        public async Task Update(Domain.Order.Entities.Order order)
        {
            var existingOrder = await _hotelDbContext.Order.FindAsync(order.Id);
            if (existingOrder != null)
            {
                _hotelDbContext.Entry(existingOrder).CurrentValues.SetValues(order);

                if (order.Address != null)
                {
                    // Atualiza ou adiciona a propriedade Address
                    _hotelDbContext.Entry(existingOrder.Address).CurrentValues.SetValues(order.Address);
                }

                await _hotelDbContext.SaveChangesAsync();
            }
        }

        public async Task SetCompletionStatus(int orderId)
        {
            var order = await _hotelDbContext.Order.FindAsync(orderId);
            if (order != null)
            {
                order.MarkAsCompleted();
                await _hotelDbContext.SaveChangesAsync();
            }
        }
    }
}
