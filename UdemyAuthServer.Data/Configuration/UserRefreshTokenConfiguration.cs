using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdemyAuthServer.Core.Models;

namespace UdemyAuthServer.Data.Configuration
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x=>x.UserId);

            builder.Property(x=>x.Code).IsRequired().HasMaxLength(200);

            builder.Property(x=>x.Expiration).IsRequired();
        }
    }
}