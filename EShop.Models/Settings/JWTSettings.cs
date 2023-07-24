using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models.Settings
{
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
