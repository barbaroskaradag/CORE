using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Context
{
    public class UdemyContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-9AIPEA5\\SQLEXPRESS; database=UdemtIdentity; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
