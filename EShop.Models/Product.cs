namespace EShop.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }

        public string StoreId { get; set; }
        public Store Store { get; set; }

        public ProductCategory Category { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }

    }
}
