using System;

using EShop.Data;
using EShop.Data.Seed;

namespace EShop.API.Extensions
{
    public static class DataSeeding
    {
        public static void UseDataSeeding(this IApplicationBuilder app, EShopDbContext context)
        {
            Seed.SeedCategoryData(context).Wait();
            Seed.SeedStoreData(context).Wait();
            Seed.SeedUsers(context).Wait();
            Seed.SeedCartProducts(context).Wait();
        }
    }
}
