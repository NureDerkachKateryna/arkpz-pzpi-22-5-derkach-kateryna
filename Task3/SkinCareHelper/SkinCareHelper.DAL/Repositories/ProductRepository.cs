using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContextEF _context;

        private readonly ILogger<ProductRepository> _logger;

        private readonly IMapper _mapper;

        public ProductRepository(DataContextEF context, ILogger<ProductRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                this._context.Products.Add(product);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            try
            {
                Product product =  await this._context.Products.SingleAsync(x => x.ProductId == productId);
                
                this._context.Products.Remove(product);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync(Expression<Func<Product, bool>> filter)
        {
            try
            {
                return await this._context.Products
                    .Where(filter)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter)
        {
            try
            {
                return await this._context.Products
                    .SingleAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                Product dbProduct = await this._context.Products.AsNoTracking().SingleAsync(p => p.ProductId == product.ProductId);

                this._mapper.Map(product, dbProduct);

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public List<Product> GetUserProductsAsync(List<Product> suitableProducts)
        {
            try
            {
                List<Product> userProducts = suitableProducts
                .GroupBy(p => p.ProductType)
                .Select(g => g.First())
                .ToList();

                return userProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}
