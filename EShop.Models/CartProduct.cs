using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class CartProduct : BaseEntity
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
