using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Cart
{
    public class CartAndProduct
    {
        [Required]
        public string ProductId { get; set; }
    }
}
