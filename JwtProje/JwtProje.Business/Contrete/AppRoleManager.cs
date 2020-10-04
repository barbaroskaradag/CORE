using JwtProje.Business.Interfaces;
using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtProje.Business.Contrete
{
    public class AppRoleManager :  GenericManager<AppRole>, IAppRoleService
    {
        private readonly IGenericDal<AppRole> _genericDal;
        public AppRoleManager(IGenericDal<AppRole> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppRole> FindByName(string roleName)
        {
            return await _genericDal.GetByFilter(I => I.Name == roleName);
        }
    }
}
