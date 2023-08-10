using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.DTO.Product
{
    public class AddProductInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Warranty { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        //[Column(TypeName = "decimal(18,2)")]
        public int Price { get; set; }

        [Required]
        public string StoreId { get; set; }

        [Required]
        public string AvatarUrl { get; set; }
    }
}
