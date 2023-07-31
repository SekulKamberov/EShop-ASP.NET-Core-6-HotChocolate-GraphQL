using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class ProductCheckSpecification : BaseSpecification<Product>
    {
        public ProductCheckSpecification(string Id) : base(p => p.Id == Id) { }
    }
}
