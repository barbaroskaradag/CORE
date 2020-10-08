using JwtProjeClint.Builders.Concrete;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JwtProjeClint.CustomFilter
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (JwtAuthorizeHelper.CheckToken(context, out string token))
            {
                var response = JwtAuthorizeHelper.GetActiveUserResponseMessage(token);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JwtAuthorizeHelper.CheckUserRole(JwtAuthorizeHelper.GetActiveUser(response), Roles, context);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("SignIn", "Account", null);
                }
                else
                {
                    var statusCode = response.StatusCode.ToString();
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("ApiError", "Account", new { code = statusCode });
                }
            }


        }
    }
}
