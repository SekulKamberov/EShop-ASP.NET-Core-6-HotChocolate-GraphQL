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
    public class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasOne(token => token.User)
                .WithMany(users => users.Tokens)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
