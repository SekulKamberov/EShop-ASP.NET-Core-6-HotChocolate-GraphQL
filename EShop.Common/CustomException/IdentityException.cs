using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Common.CustomException
{
    public class IdentityException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }
        public override string ToString()
        {
            string errors = string.Empty;
            foreach (var error in Errors)
            {
                errors += $"{error.Description} \n";
            }
            return errors;
        }
    }
}
