using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection; 

using EShop.Data;
using EShop.Models; 
using EShop.DTO.Common;
using EShop.DTO.Users;
using EShop.DTO.UsersDtos;
using EShop.Core.Services;
using EShop.DTO.Category;
using EShop.DTO.Store;

namespace EShop.Infrastructure.Mutations
{
    public class Mutations
    {
        private readonly IUserMutations userMutations;
        private readonly IProductCategoryMutations productCategoryMutations;
        private readonly IStoreMutations storeMutations;

        public Mutations(IServiceProvider serviceProvider)
        {
            userMutations = serviceProvider.GetRequiredService<IUserMutations>();
            productCategoryMutations = serviceProvider.GetRequiredService<IProductCategoryMutations>();
            storeMutations = serviceProvider.GetRequiredService<IStoreMutations>();
        }

        public async Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager, 
            CancellationToken cancellationtoken) 
                => await userMutations.AddUserAsync(input, context, userManager, cancellationtoken);
        

        public async Task<UserPayload> DeleteUserAsync(
            DeleteInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager, 
            CancellationToken cancellationtoken)
                => await userMutations.DeleteUserAsync(input, context, userManager, cancellationtoken);
        

        public async Task<LoginUserPayload> LoginUserAsync(
            LoginUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager)
                => await userMutations.LoginUserAsync(input, context, userManager);
       

        public async Task<UserPayload> UpdateUserAsync(
            UpdateUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken) 
                => await userMutations.UpdateUserAsync(input, context, userManager, cancellationtoken); 

        public async Task<CategoryPayload> AddCategory(
            AddCategoryInput input,
            [Service] EShopDbContext context) 
                => await productCategoryMutations.AddCategory(input, context); 

        public async Task<CategoryPayload> DeleteCategory(
            DeleteInput input,
            [Service] EShopDbContext context)
                => await productCategoryMutations.DeleteCategory(input, context);

        public async Task<CategoryPayload> UpdateCategory(
            UpdateCategoryInput input,
            [Service] EShopDbContext context)
                => await productCategoryMutations.UpdateCategory(input, context);

        public async Task<StorePayload> AddStore(
            AddStoreInput input,
            [Service] EShopDbContext context)
                => await storeMutations.AddStore(input, context);

        public async Task<StorePayload> DeleteStore(
            DeleteInput input,
            [Service] EShopDbContext context)       
                => await storeMutations.DeleteStore(input, context);

        public async Task<StorePayload> UpdateStore(
            UpdateStoreInput input,
            [Service] EShopDbContext context)
                => await storeMutations.UpdateStore(input, context);

    }
}
