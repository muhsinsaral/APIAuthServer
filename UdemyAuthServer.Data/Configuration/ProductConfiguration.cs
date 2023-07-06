using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyAuthServer.Core.Models;

namespace UdemyAuthServer.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x=>x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x=>x.Stock).IsRequired();

            builder.Property(x=>x.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(x=>x.UserId).IsRequired(); 
        }
    }
}