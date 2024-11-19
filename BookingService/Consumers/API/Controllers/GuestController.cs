using Application.Dtos;
using Application.Guest.Requests;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(
            ILogger<GuestController> logger,
            IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDto>> Post(GuestDto guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest,
            };

            var res = await _guestManager.CreateGuest(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND ||
                res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID ||
                res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION ||
                res.ErrorCode == Application.ErrorCode.INVALID_EMAIL ||
                res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest();
        }

        [HttpGet("{guestId}")]
        public async Task<ActionResult<GuestDto>> Get(int guestId)
        {
            var res = await _guestManager.GetGuest(guestId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDto>>> GetAll()
        {
            var res = await _guestManager.GetAllGuests();

            if (res.Any(r => !r.Success))
            {
                return NotFound(res.First(r => !r.Success));
            }

            return Ok(res.Select(r => r.Data));
        }

        [HttpPut("{guestId}")]
        public async Task<ActionResult<GuestDto>> Put(int guestId, GuestDto guest)
        {
            var guestEntity = new Domain.Entities.Guest
            {
                Id = guestId,
                Name = guest.Name,
                Email = guest.Email
                // Adicione outras propriedades conforme necessário
            };

            var res = await _guestManager.UpdateGuest(guestEntity);

            if (res.Success) return Ok(res.Data);

            if (res.ErrorCode == Application.ErrorCode.GUEST_NOT_FOUND ||
                res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID ||
                res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION ||
                res.ErrorCode == Application.ErrorCode.INVALID_EMAIL ||
                res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest();
        }

        [HttpDelete("{guestId}")]
        public async Task<ActionResult> Delete(int guestId)
        {
            var res = await _guestManager.DeleteGuest(guestId);

            if (res.Success) return NoContent();

            if (res.ErrorCode == Application.ErrorCode.GUEST_NOT_FOUND)
            {
                return NotFound(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest();
        }
    }
}
