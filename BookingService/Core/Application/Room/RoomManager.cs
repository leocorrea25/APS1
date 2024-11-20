using Application.Dtos;
using Application.Ports;
using Application.Room.Responses;
using Domain.Ports;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;
        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> CreateRoom(Domain.Entities.Room request)
        {
            try
            {
                var roomId = await _roomRepository.Create(request);
                var room = await _roomRepository.Get(roomId); // Obtenha o quarto criado

                return new RoomResponse
                {
                    Data = RoomDto.MapToDto(room), // Mapeie para RoomDto
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<RoomResponse> DeleteRoom(int roomId)
        {
            try
            {
                var room = await _roomRepository.Get(roomId);

                if (room == null)
                {
                    return new RoomResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No room record was found with the given id"
                    };
                }

                await _roomRepository.Delete(roomId);

                return new RoomResponse
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when deleting from DB"
                };
            }
        }

        public async Task<IEnumerable<RoomResponse>> GetAllRoom()
        {
            var rooms = await _roomRepository.GetAll();

            return rooms.Select(room => new RoomResponse
            {
                Data = RoomDto.MapToDto(room), // Mapeie para RoomDto
                Success = true
            }).ToList();
        }

        public async Task<RoomResponse> GetRoom(int roomId)
        {
            var room = await _roomRepository.Get(roomId);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No room record was found with the given id"
                };
            }

            return new RoomResponse
            {
                Data = RoomDto.MapToDto(room), // Mapeie para RoomDto
                Success = true,
            };
        }

        public async Task<RoomResponse> UpdateRoom(Domain.Entities.Room request)
        {
            try
            {
                var existingRoom = await _roomRepository.Get(request.Id);

                if (existingRoom == null)
                {
                    return new RoomResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCode.NOT_FOUND,
                        Message = "No room record was found with the given id"
                    };
                }

                // Atualize as propriedades do quarto diretamente
                existingRoom.Name = request.Name;
                
                // Atualize outras propriedades conforme necessário

                await _roomRepository.Update(existingRoom);

                return new RoomResponse
                {
                    Data = RoomDto.MapToDto(existingRoom), // Mapeie para RoomDto
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
