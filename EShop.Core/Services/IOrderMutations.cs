using EShop.Data; 
using EShop.DTO.Common;
using EShop.DTO.Order;

namespace EShop.Core.Services
{
    public interface IOrderMutations
    {
        Task<OrderPayload> MakeOrder(MakeOrderInput input, EShopDbContext context, string id);
        Task<OrderPayload> DeleteOrder(DeleteInput input, EShopDbContext context, string id);
        Task<OrderPayload> UpdateOrder(UpdateOrderInput input, EShopDbContext context, string id);
    }
}
