using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.BLL.Validators;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using SkinCareHelper.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly ISkincareRoutineRepository _skincareRoutineRepository;

        private readonly IUserRepository _userRepository;

        private readonly IUserService _userService;

        private readonly ILogger<ProductService> _logger;

        private readonly IMapper _mapper;

        private readonly IValidator<ProductDto> _validator;

        private readonly IPhotoAccessor _photoAccessor;

        public ProductService(IProductRepository productRepository, ISkincareRoutineRepository skincareRoutineRepository, IUserRepository userRepository, IUserService userService, ILogger<ProductService> logger, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _productRepository = productRepository;
            _skincareRoutineRepository = skincareRoutineRepository;
            _userRepository = userRepository;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _validator = new ProductValidator();
            _photoAccessor = photoAccessor;
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            try
            {
                if (!this._validator.Validate(productDto).IsValid) 
                {
                    throw new ArgumentException("Product is not valid");
                }

                Product product = new Product();                

                this._mapper.Map(productDto, product);

                product.ProductPhoto = await this._photoAccessor.AddPhoto(productDto.Photo);
                product.PhotoId = product.ProductPhoto.PhotoId;

                await this._productRepository.AddProductAsync(product);
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
                if (productId <= 0)
                {
                    throw new ArgumentNullException("Product id must be greater than 0");
                }

                await this._productRepository.DeleteProductAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                List<Product> products = new List<Product>();

                products = await this._productRepository.GetAllProductsAsync(x => x.ProductId > 0);

                List<ProductDto> productsDto = new List<ProductDto>();

                this._mapper.Map(products, productsDto);

                return productsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetBannedProductsByUserAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("User id must not be null or empty");
                }

                List<Product> products = new List<Product>();

                var productIds = (await this._userRepository.GetUserAsync(u => u.Id.Equals(userId))).Bans.Select(b => b.ProductId).ToList();

                products = await this._productRepository.GetAllProductsAsync(p => productIds.Contains(p.ProductId));

                List<ProductDto> productDtos = new List<ProductDto>();

                this._mapper.Map(products, productDtos);

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            try
            {
                if (productId <= 0)
                {
                    throw new ArgumentNullException("Product id must be greater than 0");
                }

                Product product = new Product();

                product = await this._productRepository.GetProductAsync(x => x.ProductId == productId);

                ProductDto productDto = new ProductDto();

                this._mapper.Map(product, productDto);

                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetProductsBySkincareRoutineAsync(int skincareRoutineId)
        {
            try
            {
                if (skincareRoutineId <= 0)
                {
                    throw new ArgumentNullException("Skincare routine id must be greater than 0");
                }

                List<Product> products = new List<Product>();

                var productIds = (await this._skincareRoutineRepository.GetSkincareRoutineAsync(s => s.SkincareRoutineId == skincareRoutineId)).RoutineProducts.Select(rp => rp.ProductId).ToList();

                products = await this._productRepository.GetAllProductsAsync(p => productIds.Contains(p.ProductId));

                List<ProductDto> productDtos = new List<ProductDto>();

                this._mapper.Map(products, productDtos);

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetSkincareProductsByTypeAsync(ProductTypeDto productType)
        {
            try
            {
                List<Product> products = await this._productRepository.GetAllProductsAsync(p => p.ProductType == (ProductType) productType);

                List<ProductDto> productDtos = new List<ProductDto>();

                this._mapper.Map(products, productDtos);

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetSkincareProductsByUserAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("User id must not be null or empty");
                }

                List<int> products = new List<int>();

                var skincareRoutineIds = (await this._userRepository.GetUserAsync(u => u.Id.Equals(userId))).SkincareRoutines.Select(b => b.SkincareRoutineId).ToList();

                foreach (var skincareRoutineId in skincareRoutineIds)
                {
                    List<int> productIds = (await this._skincareRoutineRepository.GetSkincareRoutineAsync(s => s.SkincareRoutineId == skincareRoutineId)).RoutineProducts.Select(r => r.ProductId).ToList();

                    foreach (var productId in productIds)
                    {
                        if (!products.Contains(productId))
                        {
                            products.Add(productId);
                        }
                    }
                }           
                
                List<Product> userProducts = await this._productRepository.GetAllProductsAsync(p => products.Contains(p.ProductId));

                List<ProductDto> productDtos = new List<ProductDto>();

                this._mapper.Map(userProducts, productDtos);

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            try
            {
                if (!this._validator.Validate(productDto).IsValid || productDto.ProductId < 0)
                {
                    throw new ArgumentNullException("Product is not valid");
                }

                var existingProduct = await this._productRepository.GetProductAsync(p => p.ProductId == productDto.ProductId);

                existingProduct.ProductName = productDto.ProductName;

                if (productDto.PhotoId is null) 
                { 
                    string previousPhotoId = existingProduct.PhotoId;
                    existingProduct.ProductPhoto = await this._photoAccessor.AddPhoto(productDto.Photo);
                    existingProduct.PhotoId = existingProduct.ProductPhoto.PhotoId;
                    await this._photoAccessor.DeletePhoto(previousPhotoId);
                }
                
                existingProduct.ProductDescription = productDto.ProductDescription;
                existingProduct.ProductType = (ProductType)productDto.ProductType;
                existingProduct.SkinType = (SkinType)productDto.SkinType;
                existingProduct.SkinIssue = (SkinIssue) productDto.SkinIssue;
                existingProduct.Brand = productDto.Brand;   

                await this._productRepository.UpdateProductAsync(existingProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetUserSuitableProductsAsync(UserDto userDto)
        {
            try
            {
                var bannedProductIds = userDto.Bans.Select(ban => ban.ProductId).ToList();

                List<Product> allProducts = await _productRepository.GetAllProductsAsync(p =>
                    (p.SkinType == (SkinType)userDto.SkinType || p.SkinType == SkinType.all) &&
                    (p.SkinIssue == (SkinIssue)userDto.SkinIssue || p.SkinIssue == SkinIssue.all) &&
                    !bannedProductIds.Contains(p.ProductId));

                List<ProductDto> productDtos = new List<ProductDto>();

                this._mapper.Map(allProducts, productDtos);

                return productDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<ProductDto>> GetUserSuitableProductsByTypeAsync(string userId, ProductTypeDto productType)
        {
            try
            {
                UserDto user = await this._userService.GetUserByIdAsync(userId);

                List<ProductDto> allUsersProducts = await GetUserSuitableProductsAsync(user);

                List<ProductDto> products = new List<ProductDto>();

                foreach (var product in allUsersProducts)
                {
                    if (product.ProductType == productType)
                        products.Add(product);
                }

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}
