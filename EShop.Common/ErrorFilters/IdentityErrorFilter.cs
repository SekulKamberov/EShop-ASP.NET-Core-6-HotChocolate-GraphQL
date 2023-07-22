using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using HotChocolate;

using EShop.Common.CustomException;

namespace EShop.Common.ErrorFilters
{
    public class IdentityErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is IdentityException ex)
                return error.WithMessage($"The following errors occured: {ex}");

            return error;
        }
    }
}
