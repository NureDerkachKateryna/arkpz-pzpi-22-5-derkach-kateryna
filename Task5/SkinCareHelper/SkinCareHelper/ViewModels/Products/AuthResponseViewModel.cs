namespace SkinCareHelper.ViewModels.Products
{
    public class AuthResponseViewModel
    {
        public UserViewModel User { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
