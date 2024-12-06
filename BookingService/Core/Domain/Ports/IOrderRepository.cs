namespace Domain.Ports
{
    public interface IOrderRepository
    {
        Task<Domain.Entities.Order> Get(int Id);
        Task<int> Create(Domain.Entities.Order order);
        Task<IEnumerable<Domain.Entities.Order>> GetAll();
        Task Update(Domain.Entities.Order order);
        Task Delete(int Id);
    }
}
