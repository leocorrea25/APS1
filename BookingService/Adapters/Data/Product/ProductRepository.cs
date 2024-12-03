using Domain.Order.Ports;
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

        async Task<Domain.Order.Entities.Product> IProductRepository.CreateProduct(Domain.Order.Entities.Product product)
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

        async Task<IEnumerable<Domain.Order.Entities.Product>> IProductRepository.GetAllProducts()
        {
            return await _hotelDbContext.Products.ToListAsync();
        }

        async Task<Domain.Order.Entities.Product> IProductRepository.GetProduct(int id)
        {
            return await _hotelDbContext.Products.FindAsync(id);
        }

        async Task<Domain.Order.Entities.Product> IProductRepository.UpdateProduct(Domain.Order.Entities.Product product)
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
