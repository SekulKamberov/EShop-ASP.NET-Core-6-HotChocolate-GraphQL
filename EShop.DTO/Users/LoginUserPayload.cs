using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Users
{
    public class LoginUserPayload
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }

        public string IsActive { get; set; }
        public string IsProfileCmpleted { get; set; }


        public AuthenticationResult TokenData { get; set; }
        public string Message { get; set; }
    }
}
