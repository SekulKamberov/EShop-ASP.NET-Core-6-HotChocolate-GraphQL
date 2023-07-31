using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class CategoryCheckSpecification : BaseSpecification<ProductCategory>
    {
        public CategoryCheckSpecification(string name) 
            : base(c => c.Name.ToLower() == name.ToLower()) { }
    }
}
