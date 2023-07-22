using Microsoft.AspNetCore.Identity; 

namespace EShop.Models
{
    public class User : IdentityUser, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }

        public bool IsActive { get; set; }
        public bool IsProfileCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }

        public ICollection<Token> Tokens { get; set; }
    }
}
