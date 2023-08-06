using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using HotChocolate.Authorization;

using EShop.Data;
using EShop.Models; 
using EShop.DTO.Common;
using EShop.DTO.Users;
using EShop.DTO.UsersDtos;
using EShop.Core.Services;
using EShop.DTO.Category;
using EShop.DTO.Store;
using EShop.DTO.Cart;
using EShop.DTO.Order;
using EShop.DTO.Product;

namespace EShop.Infrastructure.Mutations
{
    public class Mutations
    {
        private readonly IUserMutations userMutations;
        private readonly IProductCategoryMutations productCategoryMutations;
        private readonly IProductMutations productMutations;
        private readonly IStoreMutations storeMutations;
        private readonly ICartMutations cartMutations;
        private readonly IOrderMutations orderMutations;

        public Mutations(IServiceProvider serviceProvider)
        {
            userMutations = serviceProvider.GetRequiredService<IUserMutations>();
            productCategoryMutations = serviceProvider.GetRequiredService<IProductCategoryMutations>();
            productMutations = serviceProvider.GetRequiredService<IProductMutations>();
            storeMutations = serviceProvider.GetRequiredService<IStoreMutations>();
            cartMutations = serviceProvider.GetRequiredService<ICartMutations>();
            orderMutations = serviceProvider.GetRequiredService<IOrderMutations>();
        }

        public async Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager,
            [Service] RoleManager<IdentityRole> roleManager,
            CancellationToken cancellationtoken) 
                => await userMutations.AddUserAsync(input, context, userManager, roleManager, cancellationtoken);
        

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

        public async Task<ProductPayload> AddProduct(
            AddProductInput input,
            [Service] EShopDbContext context)
                => await productMutations.AddProduct(input, context);

        public async Task<ProductPayload> UpdateProduct(
            UpdateProductInput input,
            [Service] EShopDbContext context)
                => await productMutations.UpdateProduct(input, context);

        public async Task<ProductPayload> DeleteProduct(
            DeleteInput input,
            [Service] EShopDbContext context)
                => await productMutations.DeleteProduct(input, context);

        public async Task<StorePayload> AddStore(
            AddStoreInput input,
            [Service] EShopDbContext context)
                => await storeMutations.AddStore(input, context);

        public async Task<bool> DeleteStore(
            DeleteInput input,
            [Service] EShopDbContext context)       
                => await storeMutations.DeleteStore(input, context);

        public async Task<StorePayload> UpdateStore(
            UpdateStoreInput input,
            [Service] EShopDbContext context)
                => await storeMutations.UpdateStore(input, context);

        [Authorize]
        public async Task<CartPayload> AddCart(
            AddCartInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await cartMutations.AddCart(input, context, 
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        [Authorize]
        public async Task<CartPayload> DeleteCart(
            DeleteInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await cartMutations.DeleteCart(input, context,
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        [Authorize]
        public async Task<CartPayload> UpdateCart(
            UpdateCartInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await cartMutations.UpdateCart(input, context,
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        [Authorize]
        public async Task<OrderPayload> MakeOrder(
            MakeOrderInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await orderMutations.MakeOrder(input, context,
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        [Authorize]
        public async Task<OrderPayload> DeleteOrder(
            DeleteInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await orderMutations.DeleteOrder(input, context,
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }

        [Authorize]
        public async Task<OrderPayload> UpdateOrder(
            UpdateOrderInput input,
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return await orderMutations.UpdateOrder(input, context,
                user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value.ToString());
        }
    }
}
