using JwtProjeClint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.ApiServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LogIn(AppUserLogin appUserLogin);
        void LogOut();
    }
}
