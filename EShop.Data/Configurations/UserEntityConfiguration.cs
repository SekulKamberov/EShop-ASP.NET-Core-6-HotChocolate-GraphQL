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
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.Carts)
                .WithOne(Cart => Cart.User)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Orders)
                .WithOne(Order => Order.User)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
