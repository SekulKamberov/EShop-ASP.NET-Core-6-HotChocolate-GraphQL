using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class ProductCategorySpecification : BaseSpecification<ProductCategory>
    {
        public ProductCategorySpecification() { }

        public ProductCategorySpecification(string Id) 
            : base(ProductCategory => ProductCategory.Id == Id)  { }
    }
}
