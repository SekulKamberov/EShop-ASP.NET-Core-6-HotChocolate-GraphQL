using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using HotChocolate.Authorization;

using EShop.Core.Services; 
using EShop.Common.CustomException;
using EShop.Core.Repositories;
  
using EShop.Data;
using EShop.DTO.UsersDtos; 
using EShop.Models;
using EShop.DTO.Common;
using EShop.DTO.Users;
using EShop.DTO;

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
            [Service(ServiceKind.Default)] RoleManager<IdentityRole> roleManager,
            CancellationToken cancellationtoken)
        {
            var User = new User
            {
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.Email,  
                Gender = input.Gender,
                AvatarUrl = input.Avatar
            };

            var result = await userManager.CreateAsync(User, input.Password);
            if (!result.Succeeded) 
                throw new IdentityException { Errors = result.Errors };

            var userRoles = await userManager.GetRolesAsync(User);

            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                var role = await roleManager.CreateAsync(new IdentityRole("Admin"));
                if (role.Succeeded)
                {
                    if (User.Email == "se4ko@gmail.com")
                        await userManager.AddToRoleAsync(User, "Admin");
                }

            }
            else
            {
                if (User.Email == "se4ko@gmail.com")
                    await userManager.AddToRoleAsync(User, "Admin");
            }
                await context.SaveChangesAsync(cancellationtoken);

            return new UserPayload { 
                Email = User.Email, 
                FirstName = User.FirstName, 
                Id = User.Id, 
                LastName = User.LastName, 
                UserName = User.UserName,
                Gender = User.Gender,
                Avatar = User.AvatarUrl
            };
        }

        [Authorize]
        public async Task<UserPayload> DeleteUserAsync(
            DeleteInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken
            )
        {
            User user = await userManager.FindByIdAsync(input.Id);

            var result = await userManager.DeleteAsync(user); 
            if (!result.Succeeded) 
                throw new IdentityException { Errors = result.Errors }; 

            await context.SaveChangesAsync(cancellationtoken);

            return new UserPayload
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName
            };
        }

        public async Task<LoginUserPayload> LoginUserAsync(
            LoginUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager)
        {
            var user = await userManager.FindByEmailAsync(input.Email);
            LoginUserPayload response = new LoginUserPayload();

            if(user != null)
            {
                var check = await userManager.CheckPasswordAsync(user, input.Password);
                if (!check)
                {
                    response.Message = "Invalid credentials";
                    return response;
                }

                await tokenRepository.DeleteAll(tokens => tokens.UserId == user.Id);

                AuthenticationResult token = await jwtService.GetToken(user, userManager);

                response.Id = user.Id;
                response.TokenData = token;
                response.Message = "Successful";
                response.Email = user.Email;
                response.Id = user.Id;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.UserName = user.UserName;
                response.Gender = user.Gender;
                response.Avatar = user.AvatarUrl;
                return response;
            }

            response.Message = "Invalid credentials";
            return response;
        }

        [Authorize]
        public async Task<UserPayload> UpdateUserAsync(
            UpdateUserInput input,
            [Service] EShopDbContext context,
            [Service] UserManager<User> userManager,
            CancellationToken cancellationtoken)
        {
            User user = await userManager.FindByIdAsync(input.Id);
            user.FirstName = string.IsNullOrWhiteSpace(input.FirstName) ? user.FirstName : input.FirstName;
            user.LastName = string.IsNullOrWhiteSpace(input.LastName) ? user.LastName : input.LastName;
            user.Gender = string.IsNullOrWhiteSpace(input.Gender) ? user.Gender : input.Gender;

            var result = await userManager.UpdateAsync(user); 
            if (!result.Succeeded) 
                throw new IdentityException { Errors = result.Errors };

            await context.SaveChangesAsync(cancellationtoken);
            return new UserPayload { 
                Id = user.Id, 
                Email = user.Email, 
                FirstName = user.FirstName,  
                LastName = user.LastName 
            };

        }
    }
}
