namespace SkinCareHelper.ViewModels.Products
{
    public class UpdateProductViewModel : AddProductViewModel
    {
        public int ProductId { get; set;}

        public string? PhotoId { get; set; } = null!;
    }
}
