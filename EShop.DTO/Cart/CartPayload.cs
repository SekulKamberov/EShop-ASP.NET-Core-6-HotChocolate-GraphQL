using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Cart
{
    public class CartPayload
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string StoreId { get; set; }
        public ICollection<CartAndProduct> CartProducts { get; set; }
    }
}
