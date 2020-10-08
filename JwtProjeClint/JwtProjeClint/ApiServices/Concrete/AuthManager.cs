using JwtProjeClint.ApiServices.Interfaces;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JwtProjeClint.ApiServices.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IHttpContextAccessor _accessor;
        public AuthManager(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task<bool> LogIn(AppUserLogin appUserLogin)
        {
            string data = JsonConvert.SerializeObject(appUserLogin);

            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://localhost:63641/api/Auth/SignIn", stringContent);

            if(response.IsSuccessStatusCode)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(await response.Content.ReadAsStringAsync());
                
                _accessor.HttpContext.Session.SetString("token", token.Token);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void LogOut()
        {
            _accessor.HttpContext.Session.Remove("token");
        }
    }
}
