using JwtProje.Entities.Interfaces;
using System.Collections.Generic;

namespace JwtProje.Entities.Concrete
{
    public class AppRole : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AppUserRole> AppUserRoles { get; set; }
    }
}
