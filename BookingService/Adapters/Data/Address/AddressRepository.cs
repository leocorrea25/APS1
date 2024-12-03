using Domain.Order.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public AddressRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        async Task<Domain.Order.Entities.Address> IAddressRepository.CreateAddress(Domain.Order.Entities.Address address)
        {
            _hotelDbContext.Addresses.Add(address);
            await _hotelDbContext.SaveChangesAsync();
            return address;
        }

        async Task<bool> IAddressRepository.DeleteAddress(int id)
        {
            var address = await _hotelDbContext.Addresses.FindAsync(id);
            if (address == null)
            {
                return false;
            }

            _hotelDbContext.Addresses.Remove(address);
            await _hotelDbContext.SaveChangesAsync();
            return true;
        }

        async Task<Domain.Order.Entities.Address> IAddressRepository.GetAddress(int id)
        {
            return await _hotelDbContext.Addresses.FindAsync(id);
        }

        async Task<IEnumerable<Domain.Order.Entities.Address>> IAddressRepository.GetAllAddresses()
        {
            return await _hotelDbContext.Addresses.ToListAsync();
        }

        async Task<Domain.Order.Entities.Address> IAddressRepository.UpdateAddress(Domain.Order.Entities.Address address)
        {
            _hotelDbContext.Addresses.Update(address);
            await _hotelDbContext.SaveChangesAsync();
            return address;
        }
    }
}
