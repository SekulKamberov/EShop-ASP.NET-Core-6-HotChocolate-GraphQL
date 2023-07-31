using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class StoreCheckSpecification : BaseSpecification<Store>
    {
        public StoreCheckSpecification(string name) : base(s => s.Name.ToLower() == name.ToLower()) { }
    }
}
