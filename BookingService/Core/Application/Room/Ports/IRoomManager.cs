using Application.Guest.Requests;
using Application.Responses;
using Application.Room.Responses;

namespace Application.Ports
{
    public interface IRoomManager
    {
        Task<RoomResponse> CreateRoom(Domain.Entities.Room request);
        Task<RoomResponse> GetRoom(int guestId);
        Task<IEnumerable<RoomResponse>> GetAllRoom();
        Task<RoomResponse> UpdateRoom(Domain.Entities.Room request);
        Task<RoomResponse> DeleteRoom(int guestId);

    }
}
