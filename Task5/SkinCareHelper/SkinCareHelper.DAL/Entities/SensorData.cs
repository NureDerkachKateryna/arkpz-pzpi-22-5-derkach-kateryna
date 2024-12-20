using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.DAL.Entities
{
    public class SensorData
    {
        public int SensorDataId { get; set; }

        public float PHLevel { get; set; }

        public float Alcohol { get; set; } 
    }
}
