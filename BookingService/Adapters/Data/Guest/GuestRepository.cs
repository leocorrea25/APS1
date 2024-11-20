using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest

{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<int> Create(Domain.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Domain.Entities.Guest> Get(int Id)
        {
            return _hotelDbContext.Guests.Where(g => g.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Domain.Entities.Guest>> GetAll()
        {
            return await _hotelDbContext.Guests.ToListAsync();
        }

        public async Task Update(Domain.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Update(guest);
            await _hotelDbContext.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var guest = await Get(Id);
            if (guest != null)
            {
                _hotelDbContext.Guests.Remove(guest);
                await _hotelDbContext.SaveChangesAsync();
            }
        }
    }
}
