using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(Expression<Func<Product, bool>> filter);

        Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int productId);

        List<Product> GetUserProductsAsync(List<Product> suitableProducts);
    }
}
