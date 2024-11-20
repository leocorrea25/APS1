namespace Domain.Ports
{
    public interface IBookingRepository
    {
        Task<Domain.Entities.Booking> Get(int Id);
        Task<int> Create(Domain.Entities.Booking guest);
        Task<IEnumerable<Domain.Entities.Booking>> GetAll();
        Task Update(Domain.Entities.Booking guest);
        Task Delete(int Id);

    }
}
