using Microsoft.EntityFrameworkCore;

using EShop.Data;
using EShop.Models;
using EShop.Core.Services;

namespace EShop.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly EShopDbContext context;

        public ProductsRepository(EShopDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Product>> GetManyByIds(
           IReadOnlyList<string> productIds)
        {
            using (context)
            {
                return await context.Set<Product>()
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync();
            }
        }
    }
}
