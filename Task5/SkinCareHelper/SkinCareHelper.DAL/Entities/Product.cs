using SkinCareHelper.DAL.Entities.Enums;

namespace SkinCareHelper.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public Photo ProductPhoto { get; set; } = null!;

        public string PhotoId { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public ProductType ProductType { get; set; }

        public SkinType SkinType { get; set; }

        public SkinIssue SkinIssue { get; set; }

        public string Brand { get; set; } = null!;

        public List<RoutineProduct> RoutineProducts { get; set; } = new List<RoutineProduct>();

        public List<Ban> Bans { get; set; } = new List<Ban>();
    }
}
