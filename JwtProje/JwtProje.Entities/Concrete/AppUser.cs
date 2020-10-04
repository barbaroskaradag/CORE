using JwtProje.Entities.Interfaces;
using System.Collections.Generic;

namespace JwtProje.Entities.Concrete 
{
    public class AppUser : ITable
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord{ get; set; }
        public string FullName { get; set; }

        public List<AppUserRole> AppUserRoles { get; set; }
    }
}
