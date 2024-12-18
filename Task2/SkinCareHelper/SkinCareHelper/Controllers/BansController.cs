using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Entities;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BansController : ControllerBase
    {
        private readonly IBanService _banService;

        private readonly IMapper _mapper;

        private readonly ILogger<BansController> _logger;

        public BansController(IBanService banService, IMapper mapper, ILogger<BansController> logger)
        {
            _banService = banService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBan([FromBody] AddBanViewModel addBanViewModel)
        {
            try
            {
                BanDto ban = new BanDto();

                this._mapper.Map(addBanViewModel, ban);

                await this._banService.AddBanAsync(ban);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{banId}")]
        public async Task<IActionResult> DeleteBan([FromRoute] int banId)
        {
            try
            {
                await this._banService.DeleteBanAsync(banId);

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
        public async Task<IActionResult> GetAllBans()
        {
            try
            {
                List<BanDto> bansDtos = await this._banService.GetAllBansAsync();

                List<BanViewModel> bans = new List<BanViewModel>();
                
                this._mapper.Map(bansDtos, bans);

                return Ok(bans);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
