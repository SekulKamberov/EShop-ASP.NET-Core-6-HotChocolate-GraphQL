using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection; 

using EShop.Data;
using EShop.Models; 
using EShop.DTO.Common;
using EShop.DTO.Users;
using EShop.DTO.UsersDtos;
using EShop.Core.Services;

namespace EShop.Infrastructure.Mutations
{
    public class Mutations
    {
        private readonly IUserMutations userMutations;

        public Mutations(IServiceProvider serviceProvider)
        {
            userMutations = serviceProvider.GetRequiredService<IUserMutations>();
        }

        public async Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager, 
            CancellationToken cancellationtoken)
        {
            return await userMutations.AddUserAsync(input, context, userManager, cancellationtoken);
        }

        public async Task<UserPayload> DeleteUserAsync(
            DeleteInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager, 
            CancellationToken cancellationtoken)
        {
            return await userMutations.DeleteUserAsync(input, context, userManager, cancellationtoken);
        }

        public async Task<LoginUserPayload> LoginUserAsync(
            LoginUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager)
        {
            return await userMutations.LoginUserAsync(input, context, userManager);
        }

        public async Task<UserPayload> UpdateUserAsync(
            UpdateUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken)
        {
            return await userMutations.UpdateUserAsync(input, context, userManager, cancellationtoken);
        }
    }
}
