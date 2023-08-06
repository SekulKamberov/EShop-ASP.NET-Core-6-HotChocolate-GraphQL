using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Product
{
    public class AddProductInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string StoreId { get; set; }

        [Required]
        public string AvatarUrl { get; set; }
    }
}
