using Application.Guest.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
        Task<IEnumerable<GuestResponse>> GetAllGuests();
        Task<GuestResponse> UpdateGuest(Domain.Entities.Guest request);
        Task<GuestResponse> DeleteGuest(int guestId);
    }
}
