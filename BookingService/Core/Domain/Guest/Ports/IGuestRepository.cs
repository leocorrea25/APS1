namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Domain.Entities.Guest> Get(int Id);
        Task<int> Create(Domain.Entities.Guest guest);
        Task<IEnumerable<Domain.Entities.Guest>> GetAll();
        Task Update(Domain.Entities.Guest guest);
        Task Delete(int Id);
    }
}
