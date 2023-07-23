namespace EShop.Models
{
    public class Cart : BaseEntity
    {
        public string? UserId { get; set; }
        public User? User { get; set; }

        public string? StoreId { get; set; }
        public Store? Store { get; set; } 

        public ICollection<Order> Orders { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
