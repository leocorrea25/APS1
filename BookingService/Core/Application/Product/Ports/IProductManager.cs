using Domain.Order.Entities;
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
        Task<Domain.Order.Entities.Product> CreateProduct(ProductRequest product, int userId);
        Task<Domain.Order.Entities.Product> GetProduct(int productId);
        Task<IEnumerable<Domain.Order.Entities.Product>> GetAllProducts();
        Task<Domain.Order.Entities.Product> UpdateProduct(ProductRequest product, int userId);
        Task<Domain.Order.Entities.Product> DeleteProduct(int productId);
    }
}
