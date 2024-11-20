
using Domain.Enums;

namespace Application.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public RoomDto Room { get; set; }
        public GuestDto Guest { get; set; }
        public Status Status { get; set; }

        public static Domain.Entities.Booking MapToEntity(BookingDto bookingDto)
        {
            return new Domain.Entities.Booking
            {
                Id = bookingDto.Id,
                PlacedAt = bookingDto.PlacedAt,
                Start = bookingDto.Start,
                End = bookingDto.End,
                Room = RoomDto.MapToEntity(bookingDto.Room),
                Guest = GuestDto.MapToEntity(bookingDto.Guest),
                Status = bookingDto.Status
            };
        }

        public static BookingDto MapToDto(Domain.Entities.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                Room = RoomDto.MapToDto(booking.Room),
                Guest = GuestDto.MapToDto(booking.Guest),
                Status = booking.Status
            };
        }
    }
}
