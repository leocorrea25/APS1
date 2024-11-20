using Application.Guest.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IBookingManager
    {

        Task<BookingResponse> CreateBooking(Domain.Entities.Booking request);
        Task<BookingResponse> GetBooking(int guestId);
        Task<IEnumerable<BookingResponse>> GetAllBooking();
        Task<BookingResponse> UpdateBooking(Domain.Entities.Booking request);
        Task<BookingResponse> DeleteBooking(int guestId);
    }
}
