namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Domain.Entities.Room> Get(int Id);
        Task<int> Create(Domain.Entities.Room guest);
        Task<IEnumerable<Domain.Entities.Room>> GetAll();
        Task Update(Domain.Entities.Room guest);
        Task Delete(int Id);
    }
}
