using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles = "Develepor, Admin")]
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
