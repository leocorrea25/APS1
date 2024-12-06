using Domain.Entities;
using Domain.Order.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Product.Ports
{
    public interface IProductManager
    {
        Task<Domain.Entities.Product> CreateProduct(ProductRequest product, int userId);
        Task<Domain.Entities.Product> GetProduct(int productId);
        Task<IEnumerable<Domain.Entities.Product>> GetAllProducts();
        Task<IEnumerable<Domain.Entities.Product>> GetProductByUser(int id);
        Task<Domain.Entities.Product> UpdateProduct(ProductRequest product, int userId);
        Task<Domain.Entities.Product> DeleteProduct(int productId);
    }
}
