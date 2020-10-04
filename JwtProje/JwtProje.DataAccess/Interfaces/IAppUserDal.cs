using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtProje.DataAccess.Interfaces
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
