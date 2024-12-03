using Application.User.Request;
using Domain.Order.Entities;
using Domain.Order.Requests;

namespace Application.User.Ports
{
    public interface IUserManager
    {
        Task<Domain.Order.Entities.User> CreateUser(Domain.Order.Entities.User request);
        Task<Domain.Order.Entities.User> GetUser(int userId);
        Task<Domain.Order.Entities.User> Authenticate(LoginRequest loginRequest);
        Task<IEnumerable<Domain.Order.Entities.User>> GetAllUsers();
        Task<Domain.Order.Entities.User> UpdateUser(Domain.Order.Entities.User request);
        Task<Domain.Order.Entities.User> DeleteUser(int userId);
    }
}
