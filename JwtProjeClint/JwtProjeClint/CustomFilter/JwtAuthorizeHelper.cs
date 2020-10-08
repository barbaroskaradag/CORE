using JwtProjeClint.Builders.Concrete;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtProjeClint.CustomFilter
{
    public class JwtAuthorizeHelper
    {
        /// <summary>
        /// aktif userin rol bazlı durumu kontrol eder
        /// </summary>
        /// <param name="activeUser"></param>
        /// <param name="roles"></param>
        /// <param name="context"></param>
        public static void CheckUserRole(AppUser activeUser, string roles, ActionExecutingContext context)
        {
            if (String.IsNullOrEmpty(roles) == false)
            {
                Status status = null;
                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status, context);
            }
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }

        /// <summary>
        /// Response üzerinden aktif user bilgisini getirir.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static AppUser GetActiveUser(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<AppUser>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Sessiondan JWT tokenın varlığı kontrol edilir.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckToken(ActionExecutingContext context, out string token)
        {
            token = context.HttpContext.Session.GetString("token");
            if (String.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("SignIn", "Account", null);
                return false;
            }
            else
            { 
                return true;
            }
        }

        /// <summary>
        /// Aktif user endponintine istek yapar.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetActiveUserResponseMessage(string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient.GetAsync("http://localhost:63641/api/Auth/ActiveUser").Result;
        }
    }
}
