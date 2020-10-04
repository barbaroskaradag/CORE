using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.Business.StringInfos
{
    public class JwtInfo
    {
        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string SecurityKey = "isinmaprojesikey123456";
        public const double TokenExpiration = 30;
    }
}
