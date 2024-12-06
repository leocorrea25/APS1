using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> GetUser(int id);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
