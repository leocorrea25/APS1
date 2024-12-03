namespace Domain.Order.Ports
{
    public interface IOrderRepository
    {
        Task<Domain.Order.Entities.Order> Get(int Id);
        Task<int> Create(Domain.Order.Entities.Order order);
        Task<IEnumerable<Domain.Order.Entities.Order>> GetAll();
        Task Update(Domain.Order.Entities.Order order);
        Task Delete(int Id);
    }
}
