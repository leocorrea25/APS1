using Application.Dtos;
using Application.Ports;
using Application.Responses;
using Domain.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> CreateBooking(Domain.Entities.Booking request)
        {
            try
            {
                var bookingId = await _bookingRepository.Create(request);
                var booking = await _bookingRepository.Get(bookingId);

                return new BookingResponse
                {
                    Data = BookingDto.MapToDto(booking),
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<BookingResponse> DeleteBooking(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.Get(bookingId);

                if (booking == null)
                {
                    return new BookingResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No booking record was found with the given id"
                    };
                }

                await _bookingRepository.Delete(bookingId);

                return new BookingResponse
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when deleting from DB"
                };
            }
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBooking()
        {
            var bookings = await _bookingRepository.GetAll();

            return bookings.Select(booking => new BookingResponse
            {
                Data = BookingDto.MapToDto(booking),
                Success = true
            }).ToList();
        }

        public async Task<BookingResponse> GetBooking(int bookingId)
        {
            var booking = await _bookingRepository.Get(bookingId);

            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No booking record was found with the given id"
                };
            }

            return new BookingResponse
            {
                Data = BookingDto.MapToDto(booking),
                Success = true,
            };
        }

        public async Task<BookingResponse> UpdateBooking(Domain.Entities.Booking request)
        {
            try
            {
                var existingBooking = await _bookingRepository.Get(request.Id);

                if (existingBooking == null)
                {
                    return new BookingResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No booking record was found with the given id"
                    };
                }

                existingBooking.Id = request.Id;
                existingBooking.Room = request.Room;
                existingBooking.Start = request.Start;
                existingBooking.End = request.End;

                await _bookingRepository.Update(existingBooking);

                return new BookingResponse
                {
                    Data = BookingDto.MapToDto(existingBooking),
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
