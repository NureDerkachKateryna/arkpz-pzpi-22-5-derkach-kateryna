using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.BLL.Validators;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Services
{
    public class SkincareRoutineService : ISkincareRoutineService
    {
        private readonly ISkincareRoutineRepository _skincareRoutineRepository;

        private readonly IUserRepository _userRepository;

        private readonly IProductRepository _productRepository;
        
        private readonly IUserService _userService;

        private readonly IProductService _productService;

        private readonly ILogger<SkincareRoutineService> _logger;

        private readonly IMapper _mapper;

        private readonly IValidator<SkincareRoutineDto> _validator;

        public SkincareRoutineService(ISkincareRoutineRepository skincareRoutineRepository, IUserRepository userRepository, IProductRepository productRepository, ILogger<SkincareRoutineService> logger, IUserService userService, IProductService productService, IMapper mapper)
        {
            _skincareRoutineRepository = skincareRoutineRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _logger = logger;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
            _validator = new SkincareRoutineValidator();
        }

        public async Task AddProductToSkincareRoutineAsync(int skincareRoutineId, int productId)
        {
            try
            {
                await this._skincareRoutineRepository.AddProductToSkincareRoutineAsync(skincareRoutineId, productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task AddSkincareRoutineAsync(SkincareRoutineDto skincareRoutineDto)
        {
            try
            {
                if(!this._validator.Validate(skincareRoutineDto).IsValid)
                {
                    throw new ArgumentException("Skincare routine is not valid");
                }

                SkincareRoutine skincareRoutine = new SkincareRoutine();

                this._mapper.Map(skincareRoutineDto, skincareRoutine);

                await this._skincareRoutineRepository.AddSkincareRoutineAsync(skincareRoutine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task DeleteSkincareRoutineAsync(int skincareRoutineId)
        {
            try
            {
                if (skincareRoutineId <= 0)
                {
                    throw new ArgumentNullException("Skincare routine id must be greater than 0");
                }

                await this._skincareRoutineRepository.DeleteSkincareRoutineAsync(skincareRoutineId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<SkincareRoutineDto>> GetAllSkincareRoutinesAsync()
        {
            try
            {
                List<SkincareRoutine> skincareRoutine = new List<SkincareRoutine>();

                skincareRoutine = await this._skincareRoutineRepository.GetAllSkincareRoutinesAsync(x => x.SkincareRoutineId > 0);

                List<SkincareRoutineDto> skincareRoutineDtos = new List<SkincareRoutineDto>();

                this._mapper.Map(skincareRoutine, skincareRoutineDtos);

                return skincareRoutineDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<SkincareRoutineDto> GetSkincareRoutineByIdAsync(int skincareRoutineId)
        {
            try
            {
                if (skincareRoutineId <= 0)
                {
                    throw new ArgumentNullException("Skincare routine id must be greater than 0");
                }

                SkincareRoutine skincareRoutine = new SkincareRoutine();

                skincareRoutine = await this._skincareRoutineRepository.GetSkincareRoutineAsync(x => x.SkincareRoutineId == skincareRoutineId);

                SkincareRoutineDto skincareRoutineDto = new SkincareRoutineDto();

                this._mapper.Map(skincareRoutine, skincareRoutineDto);

                return skincareRoutineDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<List<SkincareRoutineDto>> GetSkincareRoutinesByUserAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("User id must not be null or empty");
                }

                List<SkincareRoutine> skincareRoutines = new List<SkincareRoutine>();

                var skincareRoutineIds = (await this._userRepository.GetUserAsync(u => u.Id.Equals(userId))).SkincareRoutines.Select(s => s.SkincareRoutineId).ToList();

                skincareRoutines = await this._skincareRoutineRepository.GetAllSkincareRoutinesAsync(s => skincareRoutineIds.Contains(s.SkincareRoutineId));

                List<SkincareRoutineDto> skincareRoutineDtos = new List<SkincareRoutineDto>();

                this._mapper.Map(skincareRoutines, skincareRoutineDtos);

                return skincareRoutineDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task UpdateSkincareRoutineAsync(SkincareRoutineDto skincareRoutineDto)
        {
            try
            {
                if (!this._validator.Validate(skincareRoutineDto).IsValid || skincareRoutineDto.SkincareRoutineId < 0)
                {
                    throw new ArgumentNullException("Skincare routine is not valid");
                }

                var existingSkincareRoutine = await this._skincareRoutineRepository.GetSkincareRoutineAsync(s => s.SkincareRoutineId == skincareRoutineDto.SkincareRoutineId);

                existingSkincareRoutine.CreationDate = DateTime.Now;
                existingSkincareRoutine.TimeOfDay = (TimeOfDay)skincareRoutineDto.TimeOfDay;
                existingSkincareRoutine.DayOfWeek = skincareRoutineDto.DayOfWeek;

                var products = await this._productRepository.GetAllProductsAsync(p => skincareRoutineDto.RoutineProducts.Select(p => p.ProductId).Contains(p.ProductId));
                existingSkincareRoutine.RoutineProducts = products.Select(p => new RoutineProduct { SkincareRoutineId = skincareRoutineDto.SkincareRoutineId, ProductId = p.ProductId }).ToList();

                await this._skincareRoutineRepository.UpdateSkincareRoutineAsync(existingSkincareRoutine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }        

        public async Task<List<SkincareRoutineDto>> GenerateUserRoutinesAsync(string userId)
        {
            try
            {
                UserDto userDto = await _userService.GetUserByIdAsync(userId);

                List<ProductDto> suitableProducts = await _productService.GetUserSuitableProductsAsync(userDto);

                List<Product> products = new List<Product>();

                this._mapper.Map(suitableProducts, products);

                List<Product> userProducts = this._productRepository.GetUserProductsAsync(products);

                List<SkincareRoutine> routines = await this._skincareRoutineRepository.GenerateSkincareRoutinesAsync(userProducts, userId);

                List<SkincareRoutineDto> routineDtos = new List<SkincareRoutineDto>();

                this._mapper.Map(routines, routineDtos);

                return routineDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task AddOrReplaceProductInRoutinesAsync(string userId, int newProductId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("User id must not be null or empty");
                }

                if (newProductId <= 0)
                {
                    throw new ArgumentNullException("Product id must be greater than 0");
                }

                ProductDto productDto = await this._productService.GetProductByIdAsync(newProductId);

                Product product = new Product();

                this._mapper.Map(productDto, product);

                List<ProductDto> productDtos = await this._productService.GetSkincareProductsByUserAsync(userId);

                List<Product> products = new List<Product>();

                this._mapper.Map(productDtos, products);

                await this._skincareRoutineRepository.AddOrReplaceProductInRoutinesAsync(userId, product, products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task RemoveProductFromRoutinesAsync(string userId, int productId)
        {
            try
            {
                await this._skincareRoutineRepository.RemoveProductFromRoutinesAsync(userId, productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}
