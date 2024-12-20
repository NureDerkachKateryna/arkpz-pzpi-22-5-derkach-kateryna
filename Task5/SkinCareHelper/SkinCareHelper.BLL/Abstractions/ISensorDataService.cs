using SkinCareHelper.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.Abstractions
{
    public interface ISensorDataService
    {
        Task AddSensorDataAsync(SensorDataDto sensorDataDto);

        Task DeleteSensorDataAsync(int sensorDataId);

        Task<SensorDataDto> GetSensorDataByIdAsync(int sensorDataId);
    }
}
