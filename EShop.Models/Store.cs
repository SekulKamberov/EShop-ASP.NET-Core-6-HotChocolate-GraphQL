using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Models
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 
        public string Description { get; set; } 
        public string AvatarUrl { get; set; }
        public string Address { get; set; }

        public string? UserId { get; set; }

        [NotMapped]
        public string ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
