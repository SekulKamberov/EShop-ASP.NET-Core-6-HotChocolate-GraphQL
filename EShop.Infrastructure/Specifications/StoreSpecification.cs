using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class StoreSpecification : BaseSpecification<Store>
    {
        public StoreSpecification() { }

        public StoreSpecification(string Id) 
            : base(x => x.Id == Id) {  } 
    }
}
