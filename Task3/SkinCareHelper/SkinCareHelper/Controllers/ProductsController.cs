using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.DAL.Entities.Enums;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddProductViewModel productViewModel)
        {
            try
            {
                ProductDto product = new ProductDto();

                this._mapper.Map(productViewModel, product);

                await this._productService.AddProductAsync(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            try
            {
                await this._productService.DeleteProductAsync(productId);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                List<ProductDto> productsDtos = await this._productService.GetAllProductsAsync();

                List<ProductViewModel> products = new List<ProductViewModel>();

                this._mapper.Map(productsDtos, products);

                return Ok(products);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetProduct/{productId}")]
        public async Task<IActionResult> GetProduct([FromRoute] int productId)
        {
            try
            {
                ProductDto productDto = await this._productService.GetProductByIdAsync(productId);

                ProductViewModel product = new ProductViewModel();

                this._mapper.Map(productDto, product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Products/Banned/{userId}")]
        public async Task<IActionResult> GetBannedProducts([FromRoute] string userId)
        {
            try
            {
                List<ProductDto> productsDtos = await this._productService.GetBannedProductsByUserAsync(userId);

                List<ProductViewModel> products = new List<ProductViewModel>();

                this._mapper.Map(productsDtos, products);

                return Ok(products);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("SkincareRoutine/{skincareRoutineId}")]
        public async Task<IActionResult> GetProductsBySkincareRoutine([FromRoute] int skincareRoutineId)
        {
            try
            {
                List<ProductDto> productsDtos = await this._productService.GetProductsBySkincareRoutineAsync(skincareRoutineId);

                List<ProductViewModel> products = new List<ProductViewModel>();

                this._mapper.Map(productsDtos, products);

                return Ok(products);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{productType}")]
        public async Task<IActionResult> GetSkincareProductsByType([FromRoute] ProductTypeDto productType)
        {
            try
            {
                List<ProductDto> productsDtos = await this._productService.GetSkincareProductsByTypeAsync(productType);

                List<ProductViewModel> products = new List<ProductViewModel>();

                this._mapper.Map(productsDtos, products);

                return Ok(products);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("SkincareProducts/{userId}")]
        public async Task<IActionResult> GetSkincareProductsByUser([FromRoute] string userId)
        {
            try
            {
                List<ProductDto> productsDtos = await this._productService.GetSkincareProductsByUserAsync(userId);

                List<ProductViewModel> products = new List<ProductViewModel>();

                this._mapper.Map(productsDtos, products);

                return Ok(products);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromForm] UpdateProductViewModel updateProductViewModel)
        {            
            try
            {
                ProductDto product = new ProductDto();

                this._mapper.Map(updateProductViewModel, product);

                await this._productService.UpdateProductAsync(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetSuitableProductsByType/{userId}/{productType}")]
        public async Task<IActionResult> GetUserSuitableProductsByType(string userId, ProductTypeDto productType)
        {
            try
            {
                List<ProductDto> products = await this._productService.GetUserSuitableProductsByTypeAsync(userId, productType);

                List<ProductViewModel> productList = new List<ProductViewModel>();

                this._mapper.Map(products, productList);

                return Ok(productList);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
