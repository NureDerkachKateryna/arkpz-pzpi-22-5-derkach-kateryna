using AutoMapper;
using Microsoft.Extensions.Logging;
using SkinCareHelper.BLL.Abstractions;
using SkinCareHelper.BLL.DTOs;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _sensorDataRepository;

        private readonly ILogger<SensorDataService> _logger;

        private readonly IMapper _mapper;

        public SensorDataService(ISensorDataRepository sensorDataRepository, ILogger<SensorDataService> logger, IMapper mapper)
        {
            _sensorDataRepository = sensorDataRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddSensorDataAsync(SensorDataDto sensorDataDto)
        {
            try
            {
                SensorData sensorData = new SensorData();

                this._mapper.Map(sensorDataDto, sensorData);

                await this._sensorDataRepository.AddSensorDataAsync(sensorData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task DeleteSensorDataAsync(int sensorDataId)
        {
            try
            {
                if (sensorDataId <= 0)
                {
                    throw new ArgumentNullException("Sensor Data id must be greater than 0");
                }

                await this._sensorDataRepository.DeleteSensorDataAsync(sensorDataId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }

        public async Task<SensorDataDto> GetSensorDataByIdAsync(int sensorDataId)
        {
            try
            {
                if (sensorDataId <= 0)
                {
                    throw new ArgumentNullException("Sensor Data id must be greater than 0");
                }

                SensorData sensorData = new SensorData();

                sensorData = await this._sensorDataRepository.GetSensorDataAsync(x => x.SensorDataId == sensorDataId);

                SensorDataDto sensorDataDto = new SensorDataDto();

                this._mapper.Map(sensorData, sensorDataDto);

                return sensorDataDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw;
            }
        }
    }
}
