using JwtProje.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace JwtProje.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(z => z.Id);
            builder.Property(z => z.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(150).IsRequired();
        }
    }
}
