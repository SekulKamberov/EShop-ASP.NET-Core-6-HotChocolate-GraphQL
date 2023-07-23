using EShop.Core.Repositories;
using EShop.Data;
using EShop.Infrastructure.Specifications;
using EShop.Models;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EShop.Infrastructure
{
    public class Query
    {
        private readonly IGenericRepository<Product> productRepo;

        public Query(IServiceProvider serviceProvider)
        {
            productRepo = serviceProvider.GetRequiredService<IGenericRepository<Product>>();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync([Service] EShopDbContext context)
        {
            return await productRepo.ListAllEntityBySpec(new ProductSpecification());
        }




    }
}
