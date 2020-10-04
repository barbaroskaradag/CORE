using JwtProje.Business.Interfaces;
using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Concrete;
using JwtProje.Entities.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtProje.Business.Contrete
{
    public class AppUserManager :  GenericManager<AppUser>, IAppUserService
    {
        private readonly IAppUserDal _appUserDal;
        public AppUserManager(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) : base(genericDal)
        {
            _appUserDal = appUserDal;
        }

        public async Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto)
        {
            var appUser = await _appUserDal.GetByFilter(I => I.UserName == appUserLoginDto.UserName);
            return appUser.PassWord == appUserLoginDto.Password ? true : false;
        }

        public async Task<AppUser> FindByUserName(string userName)
        {
            return await _appUserDal.GetByFilter(I => I.UserName == userName);
        }

        public async Task<List<AppRole>> GetRolesByUserName(string userName)
        {
            return await _appUserDal.GetRolesByUserName(userName);
        }
    }
}
