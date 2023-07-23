using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore; 

using EShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Data.Configurations
{
    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(cart => cart.Store)
                   .WithMany(Store => Store.Carts);

            builder.HasOne(cart => cart.User)
                   .WithMany(users => users.Carts);

            builder.HasMany(cart => cart.CartProducts)
                    .WithOne(cartProducts => cartProducts.Cart)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(cart => cart.Orders)
                    .WithOne(orders => orders.Cart)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
