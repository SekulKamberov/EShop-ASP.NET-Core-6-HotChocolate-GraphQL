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
    public class StoreEntityConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasMany(stores => stores.Carts)
                .WithOne(cart => cart.Store)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(stores => stores.Products)
                .WithOne(product => product.Store)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
