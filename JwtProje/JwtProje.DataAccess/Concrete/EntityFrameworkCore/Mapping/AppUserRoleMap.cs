using JwtProje.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace JwtProje.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            // iki kolonda uniq oldğu belli olur. yani 2 kolon için aynı değerler eklenemez.
            // bu sayede aynı kullanıcıya aynı rolü fazladan tanımlaması engellenir. 
            builder.HasIndex(x => new { x.AppUserId, x.AppRoleId }).IsUnique(); 
        }
    }
}
