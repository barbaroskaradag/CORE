using JwtProje.Business.Interfaces;
using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.Business.Contrete
{
    public class AppUserRoleManager :  GenericManager<AppUserRole>, IAppUserRoleService
    {
        public AppUserRoleManager(IGenericDal<AppUserRole> genericDal) : base (genericDal)
        {

        }
    }
}
