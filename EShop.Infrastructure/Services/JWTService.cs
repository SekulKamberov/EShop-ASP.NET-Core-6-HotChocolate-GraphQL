using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using EShop.Infrastructure.Specifications;
using EShop.Models.Settings;
using EShop.Core.Services;
using EShop.Core.Repositories;
using EShop.Models;
using EShop.DTO;

namespace EShop.Infrastructure.Services
{
    public class JWTService<TEntity> : IJWTService<TEntity> where TEntity : IdentityUser
    {
        private readonly IConfiguration _config;
        private readonly TokenValidationParameters _tokenParams;
        private readonly IGenericRepository<Token> _tokenRepository;
        private readonly JWTSettings _jwtSettings;

        public JWTService(IServiceProvider serviceProvider)
        {
            _config = serviceProvider.GetRequiredService<IConfiguration>();
            _tokenParams = serviceProvider.GetRequiredService<TokenValidationParameters>();
            _tokenRepository = serviceProvider.GetRequiredService<IGenericRepository<Token>>();
            _jwtSettings = serviceProvider.GetRequiredService<IOptions<JWTSettings>>().Value;
        }

        public async Task<AuthenticationResult> GetRefreshToken(
            RefreshTokenInput oldToken,
            [Service] UserManager<TEntity> _userManager)
        {
            AuthenticationResult result = new AuthenticationResult();
            var principal = GetPrincipalFromToken(oldToken.Token);
            if (principal is null)
            {
                result.Errors = new List<string> { "Invalid Token" };
                return result;
            }

            // Checks for the existence and authentication of the refresh token
            var jti = principal.Claims.SingleOrDefault(value => value.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedToken = await _tokenRepository.GetEntityBySpec(new TokenSpecification(oldToken.ReftreshToken));

            var tokenValidityCheck = storedToken is null ? false : storedToken.JwtId == jti && !storedToken.Invalidated;

            if (!tokenValidityCheck)
            {
                result.Errors = new List<string> { "Invalid Token" };
                return result;
            }

            // Checks if the token is still valid.
            var expiryDateTimeUnix =
                long.Parse(principal.Claims.SingleOrDefault(value => value.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUTC = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateTimeUnix);

            if (expiryDateTimeUTC > DateTime.UtcNow)
            {
                result.Errors = new List<string> { "Token is still valid" };
                return result;
            }

            //Creates a new token and delets the old token
            var user = await _userManager.FindByIdAsync(principal.Claims.SingleOrDefault(value => value.Type == "Id").Value);
            var newToken = await GetToken(user, _userManager);
            await _tokenRepository.DeleteEntity(storedToken);

            result.RefreshToken = newToken.RefreshToken;
            result.Token = newToken.Token;
            return result;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenParams, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception)
            {
                //Implement Nlog
            }

            throw new NotImplementedException();
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                     jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<AuthenticationResult> GetToken(
            TEntity Entity,
            [Service] UserManager<TEntity> _userManager)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Entity.Id),
                new Claim(ClaimTypes.Name, Entity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(Entity);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim("role", role));
            }

            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTConfigurations:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddDays(_jwtSettings.TokenLifeTime.Days),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));

            Token refreshToken = new Token
            {
                JwtId = token.Id,
                UserId = Entity.Id,
                RefreshToken = Guid.NewGuid().ToString()
            };
            await _tokenRepository.AddEntity(refreshToken);

            return new AuthenticationResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.RefreshToken
            };
        }
    }
}
