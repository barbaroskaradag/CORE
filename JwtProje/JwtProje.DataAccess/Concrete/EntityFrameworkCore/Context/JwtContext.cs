using JwtProje.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using JwtProje.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace JwtProje.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class JwtContext : DbContext
    {
        //Sql bağlantısı
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-9AIPEA5\\SQLEXPRESS; database=JwtDb; integrated security=true;");
        }

        //Entities içerisinde alanlara ait kısıtların tanımı yapılır.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new AppRoleMap());
            modelBuilder.ApplyConfiguration(new AppUserRoleMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }

        //DB tanıları
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
