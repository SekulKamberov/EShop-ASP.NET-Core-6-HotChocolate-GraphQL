using EShop.Models; 

namespace EShop.Infrastructure.Specifications
{
    public class StoreByUserSpecification : BaseSpecification<Store>  
    {
        public StoreByUserSpecification() { }
        public StoreByUserSpecification(string userId)
             : base(x => x.UserId == userId) { }
    }
}
