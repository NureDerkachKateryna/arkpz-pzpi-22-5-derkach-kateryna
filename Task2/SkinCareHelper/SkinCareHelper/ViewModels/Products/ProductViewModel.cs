using SkinCareHelper.DAL.Entities.Enums;

namespace SkinCareHelper.ViewModels.Products
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public PhotoViewModel ProductPhoto { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public ProductTypeDto ProductType { get; set; }

        public SkinTypeDto SkinType { get; set; } 

        public SkinIssueDto SkinIssue { get; set; }

        public string Brand { get; set; } = null!;
    }
}
