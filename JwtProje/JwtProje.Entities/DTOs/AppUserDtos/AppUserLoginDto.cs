using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Interfaces;

namespace JwtProje.Entities.Dtos.AppUserDtos
{
    public class AppUserLoginDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
