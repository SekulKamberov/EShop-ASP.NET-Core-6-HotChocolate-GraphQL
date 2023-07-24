using System.ComponentModel.DataAnnotations; 

namespace EShop.DTO
{
    public class RefreshTokenInput
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string ReftreshToken { get; set; }
    }
}
