using EShop.Core.Services;
using EShop.Data;
using EShop.DTO.UsersDtos;
using EShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
