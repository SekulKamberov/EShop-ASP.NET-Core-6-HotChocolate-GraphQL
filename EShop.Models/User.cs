using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace EShop.Models
{
    public class User : IdentityUser, IBaseEntity
    {
        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        [Required]
        public string Gender { get; set; } = default!;

        public string? AvatarUrl { get; set; }

        public bool IsActive { get; set; }
        public bool IsProfileCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }

        public ICollection<Token> Tokens { get; set; }
    }
}
