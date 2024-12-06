using Application.Struct;
using Application.User.Request;

namespace Application.User.Ports
{
    public interface IUserManager
    {
        Task<ValidationResult<Domain.Entities.User>> CreateUser(CreateUserRequest request);
        Task<ValidationResult<Domain.Entities.User>> GetUser(int userId);
        Task<ValidationResult<Domain.Entities.User>> Authenticate(UserLoginRequest loginRequest);
        Task<ValidationResult<IEnumerable<Domain.Entities.User>>> GetAllUsers();
        Task<ValidationResult<Domain.Entities.User>> UpdateUser(UpdateUserRequest request);
        Task<ValidationResult<Domain.Entities.User>> DeleteUser(int userId);
    }
}
