using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EShop.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(product => product.Store)
                .WithMany(Store => Store.Products);

            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products);

            builder.HasMany(product => product.CartProducts)
                .WithOne(CartProduct => CartProduct.Product)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
