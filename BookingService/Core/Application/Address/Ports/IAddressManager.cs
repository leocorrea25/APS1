using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Address.Ports
{
    public interface IAddressManager
    {
        Task<Domain.Entities.Address> CreateAddress(Domain.Entities.Address addressRequest);
        Task<bool> DeleteAddress(int addressId);
        Task<IEnumerable<Domain.Entities.Address>> GetAllAddresses();
        Task<Domain.Entities.Address> GetAddressForPAndN(int PostalCode, int Number);
        Task<Domain.Entities.Address> GetAddress(int addressId);
        Task<Domain.Entities.Address> UpdateAddress(Domain.Entities.Address addressRequest);
    }
}
