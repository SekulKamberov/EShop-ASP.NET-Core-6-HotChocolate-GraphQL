using EShop.Common.CustomException;
using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.Data;
using EShop.DTO.UsersDtos;
using EShop.Infrastructure.Services;
using EShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Mutations
{
    public class UserMutations : IUserMutations
    {
        private readonly IGenericRepository<Token> tokenRepository;
        private readonly IJWTService<User> jwtService;

        public UserMutations([Service] IServiceProvider serviceProvider)
        {
            jwtService = serviceProvider.GetRequiredService<IJWTService<User>>();
            tokenRepository = serviceProvider.GetRequiredService<IGenericRepository<Token>>();
        }

        public async Task<UserPayload> AddUserAsync(
            AddUserInput input, 
            [Service(ServiceKind.Default)] 
            EShopDbContext context, 
            [Service(ServiceKind.Default)] UserManager<User> userManager, 
            CancellationToken cancellationtoken)
        {
            var User = new User
            {
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                Gender = input.Gender,
                AvatarUrl = input.AvatarUrl
            };

            var result = await userManager.CreateAsync(User, input.Password);
            if (!result.Succeeded) 
                throw new IdentityException { Errors = result.Errors }; 

            await context.SaveChangesAsync(cancellationtoken);

            return new UserPayload { 
                Email = User.Email, 
                FirstName = User.FirstName, 
                Id = User.Id, 
                LastName = User.LastName, 
                UserName = User.UserName,
                Gender = User.Gender,
                AvatarUrl = User.AvatarUrl
            };
        }
    }
}
