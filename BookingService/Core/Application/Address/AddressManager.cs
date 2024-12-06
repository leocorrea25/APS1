using Application.Address.Ports;
using Domain.Ports;

namespace Application.Address
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository _addressRepository;

        public AddressManager(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        async Task<Domain.Entities.Address> IAddressManager.CreateAddress(Domain.Entities.Address addressRequest)
        {
            return await _addressRepository.CreateAddress(addressRequest);
        }

        async Task<bool> IAddressManager.DeleteAddress(int addressId)
        {
            return await _addressRepository.DeleteAddress(addressId);
        }

        async Task<Domain.Entities.Address> IAddressManager.GetAddress(int addressId)
        {
            return await _addressRepository.GetAddress(addressId);
        }

        async Task<Domain.Entities.Address> IAddressManager.GetAddressForPAndN(int postalCode, int number)
        {
            var addresses = await _addressRepository.GetAllAddresses();
            return addresses.FirstOrDefault(a => a.PostalCode == postalCode && a.Number == number);
        }

        async Task<IEnumerable<Domain.Entities.Address>> IAddressManager.GetAllAddresses()
        {
            return await _addressRepository.GetAllAddresses();
        }

        async Task<Domain.Entities.Address> IAddressManager.UpdateAddress(Domain.Entities.Address addressRequest)
        {
            return await _addressRepository.UpdateAddress(addressRequest);
        }
    }
}
