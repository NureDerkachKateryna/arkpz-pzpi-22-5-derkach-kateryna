using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Abstractions
{
    public interface IProductService
    {
        Task AddProductAsync(ProductDto productDto);

        Task UpdateProductAsync(ProductDto productDto);

        Task DeleteProductAsync(int productId);

        Task<ProductDto> GetProductByIdAsync(int productId);

        Task<List<ProductDto>> GetAllProductsAsync();

        Task<List<ProductDto>> GetSkincareProductsByUserAsync(string userId);

        Task<List<ProductDto>> GetProductsBySkincareRoutineAsync(int skincareRoutineId);

        Task<List<ProductDto>> GetSkincareProductsByTypeAsync(ProductTypeDto productType);
        
        Task<List<ProductDto>> GetBannedProductsByUserAsync(string userId);

        Task<List<ProductDto>> GetUserSuitableProductsAsync(UserDto userDto);

        Task<List<ProductDto>> GetUserSuitableProductsByTypeAsync(string userId, ProductTypeDto productType);

    }
}
