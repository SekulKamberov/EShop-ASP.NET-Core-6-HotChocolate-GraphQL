using EShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Configurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(order => order.Cart) 
                .WithMany(Cart => Cart.Orders)
                .OnDelete(DeleteBehavior.NoAction);  

            builder.HasOne(order => order.User)
                .WithMany(users => users.Orders);
        }
    }
}
