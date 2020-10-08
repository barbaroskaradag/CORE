using JwtProjeClint.Builders.Abstract;
using JwtProjeClint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.Builders.Concrete
{
    public class StatusBuilderDirector
    {
        private StatusBuilder _builder;
        public StatusBuilderDirector(StatusBuilder builder)
        {
            this._builder = builder;
        }

        public Status GenerateStatus(AppUser activeUser, string roles)
        {
            return _builder.GenerateStatus(activeUser, roles);
        }
    }
}
