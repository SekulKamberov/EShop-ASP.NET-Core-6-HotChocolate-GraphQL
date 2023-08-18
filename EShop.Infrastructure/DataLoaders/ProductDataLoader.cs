using EShop.Models;   
using EShop.Core.Services; 

namespace EShop.Infrastructure.DataLoaders
{
    public class ProductDataLoader : BatchDataLoader<string, Product>
    {
        private readonly IProductsRepository productRepository; 

        public ProductDataLoader(
             IProductsRepository _productRepository,
             IBatchScheduler batchScheduler,
             DataLoaderOptions options = null) : base(batchScheduler, options)
        {
            productRepository = _productRepository;
        }

        protected override async Task<IReadOnlyDictionary<string, Product>> LoadBatchAsync(
            IReadOnlyList<string> keys, 
            CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = await productRepository.GetManyByIds(keys);
            return products.ToDictionary(p => p.Id);
        }

        //private readonly IServiceProvider serviceProvider;

        //public ProductDataLoader(
        //     IServiceProvider _serviceProvider,
        //     IBatchScheduler batchScheduler,
        //     DataLoaderOptions? options = null) : base(batchScheduler, options)
        //{
        //    serviceProvider = _serviceProvider;
        //}

        //protected override async Task<IReadOnlyDictionary<string, Product>> LoadBatchAsync(
        //    IReadOnlyList<string> keys,
        //    CancellationToken cancellationToken)
        //{
        //    await using var scope = serviceProvider.CreateAsyncScope();
        //    var context = scope.ServiceProvider.GetRequiredService<EShopDbContext>();
        //    return await context.Products.Where(p => keys.Contains(p.Id))
        //        .ToDictionaryAsync(p => p.Id!, cancellationToken);

        //}
    }
}
