using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
          //builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(20).IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

            //builder.Property(u => u.Password).IsRequired();
            //builder.Property(u => u.Token).IsRequired();

            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
          
     

        }
    }
}