using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Order
{
    public class MakeOrderInput
    {
        [Required]
        public string CartId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; } 
    }
}
