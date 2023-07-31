using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Cart
{
    public class UpdateCartInput
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public IEnumerable<CartAndProduct> CartProducts { get; set; }
    }
}
