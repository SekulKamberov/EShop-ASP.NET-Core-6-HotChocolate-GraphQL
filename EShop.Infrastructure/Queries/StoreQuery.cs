using EShop.Data; 

namespace EShop.Infrastructure.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class StoreQuery
    {  
        public IQueryable<StoreType> GetStores([Service] EShopDbContext context)
            => context.Stores.Select(s => new StoreType()
           
            {
                Id = s.Id,
                Name = s.Name,
                PhoneNumber = s.PhoneNumber,
                Description = s.Description,
                AvatarUrl = s.AvatarUrl,
                Address = s.Address,
                UserId = s.UserId,
                Products = s.Products
            });
    }
}
