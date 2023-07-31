using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Cart
{
    public class AddCartInput
    {
        [Required]
        public string StoreId { get; set; }

        [Required]
        public ICollection<CartAndProduct> CartProducts { get; set; }
    }
}
