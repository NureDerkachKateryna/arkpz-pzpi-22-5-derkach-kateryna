using SkinCareHelper.BLL.DTOs;

namespace SkinCareHelper.ViewModels.Products
{
    public class BanViewModel
    {
        public int BanId { get; set; }

        public string UserId { get; set; } = null!;

        public UserViewModel User { get; set; } = null!;

        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; } = null!;
    }
}
