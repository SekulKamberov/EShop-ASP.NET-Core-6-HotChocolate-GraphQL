namespace EShop.Models
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
