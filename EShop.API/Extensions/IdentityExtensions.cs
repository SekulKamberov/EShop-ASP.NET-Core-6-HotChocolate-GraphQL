using Microsoft.AspNetCore.Identity;

using EShop.Models;
using EShop.Data;

namespace EShop.API.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddIdentityExtensions(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<EShopDbContext>().AddDefaultTokenProviders();
        }
    }
}
