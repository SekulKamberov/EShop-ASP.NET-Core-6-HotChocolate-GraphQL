using Microsoft.AspNetCore.Identity; 

using HotChocolate;

using EShop.Models;
using EShop.DTO.UsersDtos;
using EShop.Data;
using EShop.DTO.Common;
using EShop.DTO.Users;

namespace EShop.Core.Services
{
    public interface IUserMutations
    {
        Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager,
            [Service] RoleManager<IdentityRole> roleManager,
            CancellationToken cancellationtoken);

        Task<UserPayload> DeleteUserAsync(
            DeleteInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken);

        Task<LoginUserPayload> LoginUserAsync(
            LoginUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager);

        Task<UserPayload> UpdateUserAsync(
            UpdateUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken);
    }
}
