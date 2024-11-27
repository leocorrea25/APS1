using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository(HotelDbContext hotelDbContext) : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext = hotelDbContext;

        async Task<int> IRoomRepository.Create(Domain.Entities.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        async Task IRoomRepository.Delete(int Id)
        {
            var room = await _hotelDbContext.Rooms.FindAsync(Id);
            if (room != null)
            {
                _hotelDbContext.Rooms.Remove(room);
                await _hotelDbContext.SaveChangesAsync();
            }
        }

        async Task<Domain.Entities.Room> IRoomRepository.Get(int Id)
        {
            return await _hotelDbContext.Rooms.FindAsync(Id);
        }

        async Task<IEnumerable<Domain.Entities.Room>> IRoomRepository.GetAll()
        {
            return await _hotelDbContext.Rooms.ToListAsync();
        }

        async Task IRoomRepository.Update(Domain.Entities.Room room)
        {
            _hotelDbContext.Rooms.Update(room);
            await _hotelDbContext.SaveChangesAsync();
        }
    }
}
