using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IAddressRepository
    {
        Task<Address> CreateAddress(Address address);
        Task<Address> GetAddress(int id);
        Task<IEnumerable<Address>> GetAllAddresses();
        Task<Address> UpdateAddress(Address address);
        Task<bool> DeleteAddress(int id);
    }
}
