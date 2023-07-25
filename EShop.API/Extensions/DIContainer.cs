using EShop.Core.Repositories;
using EShop.Core.Services;
using EShop.Core.Specifications;
using EShop.Infrastructure.Mutations;
using EShop.Infrastructure.Repositories;
using EShop.Infrastructure.Services;
using EShop.Infrastructure.Specifications;
using EShop.Models.Settings;

namespace EShop.API.Extensions
{
    public static class DIContainer
    {
        public static void AddServiceRegisterations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ISpecification<>), typeof(BaseSpecification<>));
            services.AddScoped(typeof(IJWTService<>), typeof(JWTService<>));
            services.Configure<JWTSettings>(configuration.GetSection("JWTConfigurations"));
            services.AddScoped<IUserMutations, UserMutations>();

        }

    }
}
