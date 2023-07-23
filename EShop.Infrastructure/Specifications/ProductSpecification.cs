using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EShop.Models;

namespace EShop.Infrastructure.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string Id) : base(Product => Product.StoreId == Id)
        {
            AddInclude(product => product.Store);
        }
        public ProductSpecification()
        {

        }
    }
}
