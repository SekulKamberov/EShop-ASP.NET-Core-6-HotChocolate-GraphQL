using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO
{
    public class AuthenticationResult
    {
        public string Token { get; set; } 

        public string RefreshToken { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
