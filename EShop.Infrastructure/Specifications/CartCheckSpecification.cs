using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class CartCheckSpecification : BaseSpecification<Cart>
    {
        public CartCheckSpecification(string Id) : base(c => c.Id == Id) { }
    }
}
