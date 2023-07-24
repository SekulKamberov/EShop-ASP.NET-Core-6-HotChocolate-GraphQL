using HotChocolate; 

using Microsoft.Extensions.DependencyInjection;
 
using EShop.Core.Repositories;
using EShop.Data;
using EShop.Infrastructure.Specifications;
using EShop.Models;
using Microsoft.AspNetCore.Http;

namespace EShop.Infrastructure
{
    public class Query
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<Store> storeRepo;
        private readonly IGenericRepository<ProductCategory> categoryRepo;
        private readonly IGenericRepository<User> userRepo;

        public Query(IServiceProvider serviceProvider)
        {
            productRepo = serviceProvider.GetRequiredService<IGenericRepository<Product>>();
            storeRepo = serviceProvider.GetRequiredService<IGenericRepository<Store>>();
            categoryRepo = serviceProvider.GetRequiredService<IGenericRepository<ProductCategory>>();
            userRepo = serviceProvider.GetRequiredService<IGenericRepository<User>>();
        }

        [UseSorting]
        [UseFiltering]
        public async Task<IReadOnlyList<Product>> GetProductsAsync([Service] EShopDbContext context)
        {
            return await productRepo.ListAllEntityBySpec(new ProductSpecification());
        }

        [UseSorting]
        [UseFiltering]
        public async Task<IReadOnlyList<Store>> GetStoresAsync([Service] EShopDbContext context)
        {
            return await storeRepo.ListAllEntityBySpec(new StoreSpecification());
        }

        [UseSorting]
        [UseFiltering]
        public async Task<IReadOnlyList<User>> GetUsersAsync([Service] EShopDbContext context)
        {
            return await userRepo.ListAllEntityBySpec(new UserSpecification());
        }

        //[Authorized]
        public Task<User> GetUser(
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        {
            var user = contextAccessor.HttpContext.User;
            return userRepo.GetEntityBySpec(new UserSpecification(user.FindFirst(u => u.Type == "Id").Value));
        }
    }
}
