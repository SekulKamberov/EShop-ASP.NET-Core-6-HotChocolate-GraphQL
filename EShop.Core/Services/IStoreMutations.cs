using EShop.Data;
using Microsoft.AspNetCore.Http;

using EShop.DTO.Common;
using EShop.DTO.Store;
 
namespace EShop.Core.Services
{
    public interface IStoreMutations
    {
        Task<StorePayload> AddStore(AddStoreInput input, EShopDbContext context, IHttpContextAccessor contextAccessor);
        Task<StorePayload> DeleteStore(DeleteInput input, EShopDbContext context);
        Task<StorePayload> UpdateStore(UpdateStoreInput input, EShopDbContext context);
    }
}
