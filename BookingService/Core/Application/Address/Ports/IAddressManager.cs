using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Address.Ports
{
    public interface IAddressManager
    {
        Task<Domain.Order.Entities.Address> CreateAddress(Domain.Order.Entities.Address addressRequest);
        Task<bool> DeleteAddress(int addressId);
        Task<IEnumerable<Domain.Order.Entities.Address>> GetAllAddresses();
        Task<Domain.Order.Entities.Address> GetAddressForPAndN(int PostalCode, int Number);
        Task<Domain.Order.Entities.Address> GetAddress(int addressId);
        Task<Domain.Order.Entities.Address> UpdateAddress(Domain.Order.Entities.Address addressRequest);
    }
}
