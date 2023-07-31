using EShop.Data;
using EShop.DTO.Category;
using EShop.DTO.Common;

namespace EShop.Core.Services
{
    public interface IProductCategoryMutations
    {
        Task<CategoryPayload> AddCategory(AddCategoryInput input, EShopDbContext context);
        Task<CategoryPayload> DeleteCategory(DeleteInput input, EShopDbContext context);
        Task<CategoryPayload> UpdateCategory(UpdateCategoryInput input, EShopDbContext context);
    }
}
