using EShop.Data;
using EShop.DTO.Common;
using EShop.DTO.Store; 

namespace EShop.Core.Services
{
    public interface IStoreMutations
    {
        Task<StorePayload> AddStore(AddStoreInput input, EShopDbContext context);
        Task<bool> DeleteStore(DeleteInput input, EShopDbContext context);
        Task<StorePayload> UpdateStore(UpdateStoreInput input, EShopDbContext context);
    }
}
