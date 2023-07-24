using Microsoft.AspNetCore.Identity; 

using EShop.DTO;

namespace EShop.Core.Services
{
    public interface IJWTService<TEntity> where TEntity : IdentityUser
    {
        Task<AuthenticationResult> GetToken(TEntity entity, UserManager<TEntity> _userManager);
        Task<AuthenticationResult> GetRefreshToken(RefreshTokenInput refreshToken, UserManager<TEntity> _userManager); 
    }
}
