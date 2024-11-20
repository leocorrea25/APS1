using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        async Task<int> IBookingRepository.Create(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bokings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }

        async Task IBookingRepository.Delete(int Id)
        {
            var booking = await _hotelDbContext.Bokings.FindAsync(Id);
            if (booking != null)
            {
                _hotelDbContext.Bokings.Remove(booking);
                await _hotelDbContext.SaveChangesAsync();
            }
        }

        async Task<Domain.Entities.Booking> IBookingRepository.Get(int Id)
        {
            return await _hotelDbContext.Bokings.FindAsync(Id);
        }

        async Task<IEnumerable<Domain.Entities.Booking>> IBookingRepository.GetAll()
        {
            return await _hotelDbContext.Bokings.ToListAsync();
        }

        async Task IBookingRepository.Update(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bokings.Update(booking);
            await _hotelDbContext.SaveChangesAsync();
        }
    }
}
