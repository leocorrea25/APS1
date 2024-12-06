using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.User
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public UserRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        async Task<Domain.Entities.User> IUserRepository.CreateUser(Domain.Entities.User user)
        {
            _hotelDbContext.Users.Add(user);
            await _hotelDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Domain.Entities.User?> GetUserByEmail(string email)
        {
            return await _hotelDbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        async Task<bool> IUserRepository.DeleteUser(int id)
        {
            var user = await _hotelDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _hotelDbContext.Users.Remove(user);
            await _hotelDbContext.SaveChangesAsync();
            return true;
        }

        async Task<IEnumerable<Domain.Entities.User>> IUserRepository.GetAllUsers()
        {
            return await _hotelDbContext.Users.ToListAsync();
        }

        async Task<Domain.Entities.User?> IUserRepository.GetUser(int id)
        {
            return await _hotelDbContext.Users.FindAsync(id);
        }

        async Task<Domain.Entities.User?> IUserRepository.UpdateUser(Domain.Entities.User user)
        {
            _hotelDbContext.Users.Update(user);
            await _hotelDbContext.SaveChangesAsync();
            return user;
        }
    }
}
