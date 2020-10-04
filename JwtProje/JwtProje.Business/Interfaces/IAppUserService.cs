using JwtProje.Entities.Concrete;
using JwtProje.Entities.Dtos.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtProje.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> FindByUserName(string userName);
        Task<bool> CheckPassword(AppUserLoginDto appUserLoginDto);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
