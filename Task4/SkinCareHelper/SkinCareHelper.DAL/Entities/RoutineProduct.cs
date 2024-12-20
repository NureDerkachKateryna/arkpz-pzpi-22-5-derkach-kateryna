namespace SkinCareHelper.DAL.Entities
{
    public class RoutineProduct
    {
        public int RoutineProductId { get; set; }

        public int SkincareRoutineId { get; set; }

        public SkincareRoutine SkincareRoutine { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

    }
}
