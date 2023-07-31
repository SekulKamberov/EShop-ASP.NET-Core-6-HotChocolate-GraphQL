using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EShop.Models;


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
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cart => cart.Orders)
                    .WithOne(orders => orders.Cart)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
