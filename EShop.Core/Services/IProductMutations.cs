using HotChocolate;

using EShop.Data;
using EShop.DTO.Common;
using EShop.DTO.Product;

namespace EShop.Core.Services
{
    public interface IProductMutations
    {
        Task<ProductPayload> AddProduct(AddProductInput input, [Service] EShopDbContext context);
        Task<ProductPayload> DeleteProduct(DeleteInput input, [Service] EShopDbContext context);
        Task<ProductPayload> UpdateProduct(UpdateProductInput input, [Service] EShopDbContext context);
    }
}
