using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using HotChocolate.Authorization;
 
using EShop.Core.Repositories;
using EShop.Data;
using EShop.Infrastructure.Specifications;
using EShop.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync([Service] EShopDbContext context)
        {
            return await categoryRepo.ListAllEntityBySpec(new CategorySpecification());
        }

        [Authorize]
        [UseSorting]
        [UseFiltering]
        public async Task<IReadOnlyList<Store>> GetStoresAsync(
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor)
        { 
            return await storeRepo.ListAllEntityBySpec(new StoreByUserSpecification(GetUserId(contextAccessor)));
        } 

        [UseSorting]
        [UseFiltering]
        public async Task<IReadOnlyList<User>> GetUsersAsync([Service] EShopDbContext context)
        {
            return await userRepo.ListAllEntityBySpec(new UserSpecification());
        }

        [Authorize]
        public Task<User> GetUser(
            [Service] EShopDbContext context,
            [Service] IHttpContextAccessor contextAccessor) 
            => userRepo.GetEntityBySpec(new UserSpecification(GetUserId(contextAccessor))); 

        private static string GetUserId(IHttpContextAccessor contextAccessor)
            => contextAccessor.HttpContext.User.Claims
                .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)
                .Value.ToString();
    }
}
