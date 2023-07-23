using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; 

namespace EShop.Core.Specifications
{
    public interface ISpecification<TModel> where TModel : class
    {
        Expression<Func<TModel, bool>> Criteria { get; }

        List<Expression<Func<TModel, object>>> Includes { get; }















    }
}
