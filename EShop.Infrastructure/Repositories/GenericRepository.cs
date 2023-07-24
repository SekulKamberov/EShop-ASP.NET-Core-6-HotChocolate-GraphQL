﻿using EShop.Core.Repositories;
using EShop.Core.Specifications;
using EShop.Data; 
using EShop.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EShopDbContext context;

        public GenericRepository(EShopDbContext _context)
        {
            context = _context;
        } 

        public async Task<IReadOnlyList<TEntity>> ListAllEntityBySpec(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        } 

        public async Task<TEntity> GetEntityBySpec(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.EvaluateQuery(context.Set<TEntity>().AsQueryable(), spec);
        }

        public Task<bool> AddEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
