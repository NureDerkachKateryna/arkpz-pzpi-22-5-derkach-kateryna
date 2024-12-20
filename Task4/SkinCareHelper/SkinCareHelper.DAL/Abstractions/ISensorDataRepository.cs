using SkinCareHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Abstractions
{
    public interface ISensorDataRepository
    {
        Task<SensorData> GetSensorDataAsync(Expression<Func<SensorData, bool>> filter);

        Task AddSensorDataAsync(SensorData sensorData);

        Task DeleteSensorDataAsync(int sensorDataId);
    }
}
