using JwtProje.Business.Interfaces;
using JwtProje.Business.StringInfos;
using JwtProje.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtProje.Business.Contrete
{
    public class JwtManager : IJwtServices
    {
        public string GenerateJwtToken(AppUser appUser, List<AppRole> roles)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtInfo.Issuer,
                audience: JwtInfo.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(JwtInfo.TokenExpiration),
                signingCredentials: signingCredentials,
                claims: GetClaims(appUser, roles));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private List<Claim> GetClaims(AppUser appUser, List<AppRole> roles)
        {
            if (appUser == null)
                return null;

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));

            if (roles != null)
            {
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name));
                }
            }

            return claims;
        }
    }
}
