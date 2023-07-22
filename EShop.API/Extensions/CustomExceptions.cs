using EShop.Common.ErrorFilters;

namespace EShop.API.Extensions
{
    public static class CustomExceptions
    {
        public static void AddCustomErrorFilters(this IServiceCollection services)
        {
            services.AddErrorFilter<IdentityErrorFilter>(); 
        }
    }
}
