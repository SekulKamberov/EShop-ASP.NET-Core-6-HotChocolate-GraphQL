using EShop.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Specifications
{
    public sealed class SpecificationEvaluator<TModel> where TModel : class
    {
        public static IQueryable<TModel> EvaluateQuery(IQueryable<TModel> query, ISpecification<TModel> spec)
        {
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include)); 

            return query;
        }
    }
}
