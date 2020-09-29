using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApiCore.Services
{
    public interface IUserService
    {
        string GetName(string name);
    }
}
