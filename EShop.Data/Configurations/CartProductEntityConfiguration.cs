using System; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EShop.Models;

namespace EShop.Data.Configurations
{
    public class CartProductEntityConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder
                .HasKey(cartProduct => new { cartProduct.ProductId, cartProduct.CartId });

            builder.HasOne(cartProduct => cartProduct.Product)
                 .WithMany(products => products.CartProducts)
                 .HasForeignKey(cartProduct => cartProduct.ProductId);

            builder.HasOne(cartProduct => cartProduct.Cart)
                .WithMany(cart => cart.CartProducts)
                .HasForeignKey(cartproduct => cartproduct.CartId);
        }
    }
}
