using EShop.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> ListAllEntityBySpec(ISpecification<TEntity> spec);
        Task<TEntity> GetEntityBySpec(ISpecification<TEntity> spec);
        Task<bool> AddEntity(TEntity entity);
        Task<bool> UpdateEntity(TEntity entity);
        Task<bool> DeleteEntity(TEntity entity);



    }
}
