using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Interfaces;

namespace JwtProje.Entities.Token
{
    public class JwtAccessToken : IToken
    {
        public string Token { get; set; }
    }
}
