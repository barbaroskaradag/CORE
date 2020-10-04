using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProje.WebApi.CustomFilters
{
    /// <summary>
    /// modellerin isvalid durumu kontrolleri yapıalcak.
    /// </summary>
    public class ValidModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //SelectMany aynı tip veriyi teke düşürür
            //var mesajlar = context.ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage));

            if(context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
