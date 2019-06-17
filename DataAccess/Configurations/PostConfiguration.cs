using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.Property(p => p.Title).HasMaxLength(60).IsRequired();
            builder.HasIndex(p => p.Title).IsUnique();

            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.ImagePath).IsRequired();


            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
