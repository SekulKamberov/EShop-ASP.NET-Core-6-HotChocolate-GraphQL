using EShop.Core.Repositories;
using EShop.Core.Specifications;
using EShop.Infrastructure.Repositories;
using EShop.Infrastructure.Specifications;

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
        }

    }
}
