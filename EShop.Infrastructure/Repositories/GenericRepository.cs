﻿using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using EShop.Data;
using EShop.Core.Repositories;
using EShop.Core.Specifications;
using EShop.Infrastructure.Specifications; 

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

        public async Task<bool> AddEntity(TEntity entity)
        { 
            try 
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
            } catch(Exception ex)  {
                return true;
            }
            return true;
        }

        public Task<bool> UpdateEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAll(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = context.Set<TEntity>().Where(predicate).AsQueryable();
            if(entitiesToDelete.Any())
            {
                context.Set<TEntity>().RemoveRange(entitiesToDelete);
                return await context.SaveChangesAsync() > 0;
            }
            else { return false; }
            
        }
    }
}
