using JwtProje.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.Business.Interfaces
{
    public interface IJwtServices
    {
        string GenerateJwtToken(AppUser appUser, List<AppRole> roles);
    }
}
