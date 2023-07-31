using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Common.CustomException
{
    public class ModelExceptions : Exception
    {
        public string DefaultError { get; set; }
    }
}
