using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Order
{
    public class UpdateOrderInput
    {
        [Required]
        public string Id { get; set; }

        public string CartId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
