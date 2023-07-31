using EShop.Data;
using EShop.DTO.Cart;
using EShop.DTO.Common; 

namespace EShop.Core.Services
{
    public interface ICartMutations
    {
        Task<CartPayload> AddCart(AddCartInput input, EShopDbContext context, string id);
        Task<CartPayload> DeleteCart(DeleteInput input, EShopDbContext context, string Id);
        Task<CartPayload> UpdateCart(UpdateCartInput input, EShopDbContext context, string id);
    }
}
