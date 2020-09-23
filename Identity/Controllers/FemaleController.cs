using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Policy = "FemalePolicy")]
    public class FemaleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public FemaleController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddClaim(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id); //IQueryable sayesinde yazılan koşullar sorgu yerine geçmektedir.

            if ((await _userManager.GetClaimsAsync(user)).Count == 0)
            {
                Claim claim = new Claim("gender", "female");
                await _userManager.AddClaimAsync(user, claim);
            }

            var lst = await _userManager.GetClaimsAsync(user);

            return RedirectToAction("UserList","Rol");
        }
    }
}
