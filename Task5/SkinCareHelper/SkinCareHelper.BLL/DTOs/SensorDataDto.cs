using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class SensorDataDto
    {
        public int SensorDataId { get; set; }

        public float PHLevel { get; set; }

        public float Alcohol { get; set; } 
    }
}
