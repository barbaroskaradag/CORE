using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApiCore.Services
{
    public class UserManager : IUserService
    {
        public string GetName(string name)
        {
            return name;
        }
    }
}
