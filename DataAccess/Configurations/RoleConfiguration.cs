﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(r => r.Name).HasMaxLength(20).IsRequired();
            builder.HasIndex(r => r.Name).IsUnique();

            builder.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
