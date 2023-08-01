using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class OrderCheckSpecification : BaseSpecification<Order>
    {
        public OrderCheckSpecification(string Id) : base(o => o.Id == Id) { }
    }
}
