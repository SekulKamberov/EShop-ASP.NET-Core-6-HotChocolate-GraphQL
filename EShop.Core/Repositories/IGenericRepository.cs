using System.Linq.Expressions;

using EShop.Core.Specifications;

namespace EShop.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> ListAllEntityBySpec(ISpecification<TEntity> spec);
        Task<TEntity> GetEntityBySpec(ISpecification<TEntity> spec);
        Task<bool> AddEntity(TEntity entity);
        Task<bool> UpdateEntity(TEntity entity);
        Task<bool> DeleteEntity(TEntity entity);
        Task<bool> DeleteAll(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AddRangeAsync(ICollection<TEntity> entities);
    }
}
