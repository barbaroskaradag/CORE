using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtProjeClint.ApiServices.Concrete;
using JwtProjeClint.ApiServices.Interfaces;
using JwtProjeClint.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtProjeClint.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogin appUserLogin)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.LogIn(appUserLogin))
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ve şifre hatalı");
                }
            }
            return View(appUserLogin);
        }
    }
}
