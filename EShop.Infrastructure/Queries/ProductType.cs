using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Queries
{
    public class ProductType : ISearchResultType
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Warranty { get; set; } 

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public string AvatarUrl { get; set; }
    }
}
