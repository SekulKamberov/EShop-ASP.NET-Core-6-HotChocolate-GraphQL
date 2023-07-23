using EShop.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Specifications
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<TEntity, bool>> criteria) { Criteria = criteria; }    

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }






    }
}
