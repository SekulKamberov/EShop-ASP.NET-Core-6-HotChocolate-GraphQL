using Microsoft.AspNetCore.Identity; 

using HotChocolate;

using EShop.Models;
using EShop.DTO.UsersDtos;
using EShop.Data;

namespace EShop.Core.Services
{
    public interface IUserMutations
    {
        Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service] EShopDbContext context, 
            [Service] UserManager<User> userManager, 
            CancellationToken cancellationtoken);
    }
}
