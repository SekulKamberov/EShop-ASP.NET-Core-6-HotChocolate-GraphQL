using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Store
{
    public class StorePayload
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string UserId { get; set; }
    }
}
