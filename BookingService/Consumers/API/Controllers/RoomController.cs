using Application.Ports;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Domain.Room.ValueObjects;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomService;

        public RoomController(
            ILogger<RoomController> logger,
            IRoomManager roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Post(RoomDto roomDto)
        {
            var room = RoomDto.MapToEntity(roomDto);

            var res = await _roomService.CreateRoom(room);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND ||
                res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID ||
                res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION ||
                res.ErrorCode == Application.ErrorCode.INVALID_EMAIL ||
                res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned: {ErrorCode}", res.ErrorCode);
            return BadRequest();
        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> Get(int roomId)
        {
            var res = await _roomService.GetRoom(roomId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        {
            var res = await _roomService.GetAllRoom();

            if (res.Any(r => !r.Success))
            {
                return NotFound(res.First(r => !r.Success));
            }

            return Ok(res.Select(r => r.Data));
        }

        [HttpPut("{roomId}")]
        public async Task<ActionResult<RoomDto>> Put(int roomId, RoomDto roomDto)
        {
            var room = RoomDto.MapToEntity(roomDto);
            room.Id = roomId;

            var res = await _roomService.UpdateRoom(room);

            if (res.Success) return Ok(res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND ||
                res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID ||
                res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION ||
                res.ErrorCode == Application.ErrorCode.INVALID_EMAIL ||
                res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned: {ErrorCode}", res.ErrorCode);
            return BadRequest();
        }

        [HttpDelete("{roomId}")]
        public async Task<ActionResult> Delete(int roomId)
        {
            var res = await _roomService.DeleteRoom(roomId);

            if (res.Success) return NoContent();

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
            {
                return NotFound(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned: {ErrorCode}", res.ErrorCode);
            return BadRequest();
        }
    }
}
