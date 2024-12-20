using SkinCareHelper.DAL.Entities.Enums;

namespace SkinCareHelper.DAL.Entities
{
    public class SkincareRoutine
    {
        public int SkincareRoutineId { get; set; }

        public TimeOfDay TimeOfDay { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;

        public List<RoutineProduct> RoutineProducts { get; set; } = new List<RoutineProduct>();
    }
}
