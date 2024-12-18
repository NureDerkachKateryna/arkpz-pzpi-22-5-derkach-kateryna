using SkinCareHelper.BLL.DTOs.Enums;
using SkinCareHelper.BLL.DTOs;

namespace SkinCareHelper.ViewModels.Products
{
    public class SkincareRoutineViewModel
    {
        public int SkincareRoutineId { get; set; }

        public TimeOfDayDto TimeOfDay { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; } = null!;

        public UserViewModel User { get; set; } = null!;
    }
}
