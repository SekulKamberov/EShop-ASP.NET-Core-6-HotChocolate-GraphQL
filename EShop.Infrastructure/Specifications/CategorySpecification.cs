using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class CategorySpecification : BaseSpecification<ProductCategory>
    { 
            public CategorySpecification() { }

            public CategorySpecification(string Id) : base(User => User.Id == Id) { } 
    }
}
