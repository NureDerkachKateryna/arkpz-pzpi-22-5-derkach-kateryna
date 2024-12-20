using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.ViewModels.Products;

namespace SkinCareHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkincareRoutinesController : ControllerBase
    {
        private readonly ISkincareRoutineService _skincareRoutineService;

        private readonly IMapper _mapper;

        private readonly ILogger<SkincareRoutinesController> _logger;

        public SkincareRoutinesController(ISkincareRoutineService skincareRoutineService, IMapper mapper, ILogger<SkincareRoutinesController> logger)
        {
            _skincareRoutineService = skincareRoutineService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddSkincareRoutine([FromBody] AddSkincareRoutineViewModel addSkincareRoutineViewModel)
        {
            try
            {
                SkincareRoutineDto skincareRoutine = new SkincareRoutineDto();

                this._mapper.Map(addSkincareRoutineViewModel, skincareRoutine);

                await this._skincareRoutineService.AddSkincareRoutineAsync(skincareRoutine);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("{skincareRoutineId}/{productId}")]
        public async Task<IActionResult> AddProductToSkincareRoutine([FromRoute] int skincareRoutineId, [FromRoute] int productId)
        {
            try
            {    
                await this._skincareRoutineService.AddProductToSkincareRoutineAsync(skincareRoutineId, productId);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{skincareRoutineId}")]
        public async Task<IActionResult> DeleteSkincareRoutine([FromRoute] int skincareRoutineId)
        {
            try
            {
                await this._skincareRoutineService.DeleteSkincareRoutineAsync(skincareRoutineId);

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
        public async Task<IActionResult> GetAllSkincareRoutines()
        {
            try
            {
                List<SkincareRoutineDto> skincareRoutinesDtos = await this._skincareRoutineService.GetAllSkincareRoutinesAsync();

                List<SkincareRoutineViewModel> skincareRoutines = new List<SkincareRoutineViewModel>();

                this._mapper.Map(skincareRoutinesDtos, skincareRoutines);

                return Ok(skincareRoutines);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetSkincareRoutine/{skincareRoutineId}")]
        public async Task<IActionResult> GetSkincareRoutineById([FromRoute] int skincareRoutineId)
        {
            try
            {
                SkincareRoutineDto skincareRoutineDto = await this._skincareRoutineService.GetSkincareRoutineByIdAsync(skincareRoutineId);

                SkincareRoutineViewModel skincareRoutine = new SkincareRoutineViewModel();

                this._mapper.Map(skincareRoutineDto, skincareRoutine);

                return Ok(skincareRoutine);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Users/{userId}")]
        public async Task<IActionResult> GetSkincareRoutinesByUser([FromRoute] string userId)
        {
            try
            {
                List<SkincareRoutineDto> skincareRoutinesDtos = await this._skincareRoutineService.GetSkincareRoutinesByUserAsync(userId);

                List<SkincareRoutineViewModel> skincareRoutines = new List<SkincareRoutineViewModel>();

                this._mapper.Map(skincareRoutinesDtos, skincareRoutines);

                return Ok(skincareRoutines);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditSkincareRoutine([FromBody] UpdateSkincareRoutineViewModel updateSkincareRoutineViewModel)
        {
            try
            {
                SkincareRoutineDto skincareRoutine = new SkincareRoutineDto();

                this._mapper.Map(updateSkincareRoutineViewModel, skincareRoutine);

                skincareRoutine.SkincareRoutineId = updateSkincareRoutineViewModel.SkincareRoutineId;

                await this._skincareRoutineService.UpdateSkincareRoutineAsync(skincareRoutine);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GenerateRoutines/{userId}")]
        public async Task<IActionResult> GenerateUserRoutines(string userId)
        {
            try
            {
                List<SkincareRoutineDto> skincareRoutinesDtos = await this._skincareRoutineService.GenerateUserRoutinesAsync(userId);

                List<SkincareRoutineViewModel> skincareRoutines = new List<SkincareRoutineViewModel>();

                this._mapper.Map(skincareRoutinesDtos, skincareRoutines);

                return Ok(skincareRoutines);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("AddOrReplaceProduct/{userId}/{productId}")]
        public async Task<IActionResult> AddOrReplaceProductInRoutines(string userId, int productId)
        {
            try
            {
                await this._skincareRoutineService.AddOrReplaceProductInRoutinesAsync(userId, productId);

                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("RemoveProductFromRoutines/{userId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromRoutines(string userId, int productId)
        {
            try
            {
                await this._skincareRoutineService.RemoveProductFromRoutinesAsync(userId, productId);

                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
