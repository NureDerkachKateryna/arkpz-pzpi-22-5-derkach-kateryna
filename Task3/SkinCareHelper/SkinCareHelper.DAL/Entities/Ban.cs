namespace SkinCareHelper.DAL.Entities
{
    public class Ban
    {
        public int BanId { get; set; }

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
