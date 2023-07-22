using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EShop.Models;

namespace EShop.Data.Configurations
{
    public class ProductCategoryEntityConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasMany(productCategory => productCategory.Products)
                .WithOne(ProductCategory => ProductCategory.Category)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
