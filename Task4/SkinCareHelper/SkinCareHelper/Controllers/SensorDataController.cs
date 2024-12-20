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
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        private readonly IMapper _mapper;

        private readonly ILogger<SensorDataController> _logger;

        public SensorDataController(ISensorDataService sensorDataService, IMapper mapper, ILogger<SensorDataController> logger)
        {
            _sensorDataService = sensorDataService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddSensorData([FromBody] AddSensorDataViewModel addSensorDataViewModel)
        {
            try
            {
                SensorDataDto sensorData = new SensorDataDto();

                this._mapper.Map(addSensorDataViewModel, sensorData);

                await this._sensorDataService.AddSensorDataAsync(sensorData);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{sensorDataId}")]
        public async Task<IActionResult> DeleteSensorData([FromRoute] int sensorDataId)
        {
            try
            {
                await this._sensorDataService.DeleteSensorDataAsync(sensorDataId);

                return NoContent();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSensorData/{sensorDataId}")]
        public async Task<IActionResult> GetSensorData([FromRoute] int sensorDataId)
        {
            try
            {
                SensorDataDto sensorDataDto = await this._sensorDataService.GetSensorDataByIdAsync(sensorDataId);

                SensorDataViewModel sensorData = new SensorDataViewModel();

                this._mapper.Map(sensorDataDto, sensorData);

                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
