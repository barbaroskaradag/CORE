using JwtProjeClint.Builders.Abstract;
using JwtProjeClint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.Builders.Concrete
{
    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            Status status = new Status();
            status.AccessStatus = activeUser.Roles.Contains(roles);
            return status;
        }
    }
}
