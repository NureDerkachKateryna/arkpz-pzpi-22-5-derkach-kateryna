using SkinCareHelper.BLL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinCareHelper.BLL.DTOs
{
    public class SkincareRoutineDto
    {
        public int SkincareRoutineId { get; set; }

        public TimeOfDayDto TimeOfDay { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public List<RoutineProductDto> RoutineProducts { get; set; } = new List<RoutineProductDto>();
    }
}
