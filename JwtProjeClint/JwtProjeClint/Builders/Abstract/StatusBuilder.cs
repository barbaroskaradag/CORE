using JwtProjeClint.Builders.Concrete;
using JwtProjeClint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.Builders.Abstract
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(AppUser activeUser, string roles);
    }
}
