using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using EShop.Models;
using EShop.Data.Configurations;

namespace EShop.Data
{
    public class EShopDbContext : IdentityDbContext<User>
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CartEntityConfiguration());
            builder.ApplyConfiguration(new StoreEntityConfiguration());
            builder.ApplyConfiguration(new CartProductEntityConfiguration());
            builder.ApplyConfiguration(new OrderEntityConfiguration());
            builder.ApplyConfiguration(new TokenEntityConfiguration());
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new ProductEntityConfiguration());
        }
    }
}
