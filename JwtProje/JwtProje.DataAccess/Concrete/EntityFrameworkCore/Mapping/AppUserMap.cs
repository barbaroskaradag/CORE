using JwtProje.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique(); //uniq user name için

            builder.Property(x => x.PassWord).HasMaxLength(100).IsRequired();

            builder.Property(x => x.FullName).HasMaxLength(150);

            /* ilişki kurma işlemi */
            builder.HasMany(x => x.AppUserRoles) //list
                .WithOne(x => x.AppUser) //property
                .HasForeignKey(x => x.AppUserId) // ilişki id
                .OnDelete(DeleteBehavior.Cascade); //Cascade biri silindi mi ilişkide silinsin
        }
    }
}
