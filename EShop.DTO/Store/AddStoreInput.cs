using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO.Store
{
    public class AddStoreInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
