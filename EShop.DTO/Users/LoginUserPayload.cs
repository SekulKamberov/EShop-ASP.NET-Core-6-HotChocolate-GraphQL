using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Users
{
    public class LoginUserPayload
    {
        public string Email { get; set; }
        public AuthenticationResult TokenData { get; set; }
        public string Message { get; set; }
    }
}
