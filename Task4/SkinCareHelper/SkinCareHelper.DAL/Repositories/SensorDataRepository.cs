using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinCareHelper.DAL.Abstractions;
using SkinCareHelper.DAL.DbContexts;
using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly DataContextEF _context;

        private readonly ILogger<SensorDataRepository> _logger;

        private readonly IMapper _mapper;

        public SensorDataRepository(DataContextEF context, ILogger<SensorDataRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddSensorDataAsync(SensorData sensorData)
        {
            try
            {
                this._context.SensorData.Add(sensorData);
                await this._context.SaveChangesAsync();
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
                SensorData sensorData = await this._context.SensorData.SingleAsync(x => x.SensorDataId == sensorDataId);

                this._context.SensorData.Remove(sensorData);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<SensorData> GetSensorDataAsync(Expression<Func<SensorData, bool>> filter)
        {
            try
            {
                return await this._context.SensorData
                    .SingleAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
