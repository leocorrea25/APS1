using Application.Dtos;
using Application.Guest.Requests;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(ILogger<BookingController> logger, IBookingManager bookingManager)
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto request)
        {
            var booking = BookingDto.MapToEntity(request);
            var response = await _bookingManager.CreateBooking(booking);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var response = await _bookingManager.DeleteBooking(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            var response = await _bookingManager.GetAllBooking();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var response = await _bookingManager.GetBooking(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookingDto request)
        {
            var booking = BookingDto.MapToEntity(request);
            var response = await _bookingManager.UpdateBooking(booking);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
