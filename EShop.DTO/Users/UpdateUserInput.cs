using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Users
{
    public class UpdateUserInput
    {
        public string Id { get; set; } 
        public string Email { get; set; } = default!; 
        public string FirstName { get; set; } = default!; 
        public string LastName { get; set; } = default!;  
        public string Password { get; set; } = default!; 
        public string Gender { get; set; } = default!; 
        public string UserName
        {
            get { return Email; }
            set { }
        }

        public string? Avatar { get; set; }
    }
}
