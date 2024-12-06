using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public ProductRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        async Task<Domain.Entities.Product> IProductRepository.CreateProduct(Domain.Entities.Product product)
        {
            _hotelDbContext.Products.Add(product);
            await _hotelDbContext.SaveChangesAsync();
            return product;
        }

        async Task<bool> IProductRepository.DeleteProduct(int id)
        {
            var product = await _hotelDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _hotelDbContext.Products.Remove(product);
            await _hotelDbContext.SaveChangesAsync();
            return true;
        }

        async Task<IEnumerable<Domain.Entities.Product>> IProductRepository.GetAllProducts()
        {
            return await _hotelDbContext.Products.ToListAsync();
        }

        async Task<Domain.Entities.Product> IProductRepository.GetProduct(int id)
        {
            return await _hotelDbContext.Products.FindAsync(id);
        }

        async Task<Domain.Entities.Product> IProductRepository.UpdateProduct(Domain.Entities.Product product)
        {
            var existingProduct = await _hotelDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return null;
            }

            _hotelDbContext.Entry(product).State = EntityState.Modified;
            await _hotelDbContext.SaveChangesAsync();
            return product;
        }
    }
}
