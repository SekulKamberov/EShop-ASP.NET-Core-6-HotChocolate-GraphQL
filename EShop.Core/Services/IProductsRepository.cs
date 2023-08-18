using EShop.Models; 

namespace EShop.Core.Services
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetManyByIds(IReadOnlyList<string> productIds);
    }
}
