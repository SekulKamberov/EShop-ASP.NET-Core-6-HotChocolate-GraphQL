using System.ComponentModel.DataAnnotations;

namespace EShop.DTO.UsersDtos
{
    public class AddUserInput
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        public string Gender { get; set; } = default!;  

        public string UserName
        {
            get { return Email; }
            set { }
        }

        public string? AvatarUrl { get; set; }
    }
}
