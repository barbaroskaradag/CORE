using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize] // sisteme kullanıcı girmiş mi kontorlü
    public class PanelController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserUpdateViewModel model = new AppUserUpdateViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                SurName = user.SurName,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUserUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(model.Picture != null)
                {
                    string uygulamaAdres = Directory.GetCurrentDirectory();
                    string uzanti = Path.GetExtension(model.Picture.FileName);
                    string resimAdi = Guid.NewGuid().ToString() + uzanti;

                    string kaydedilecekYer = uygulamaAdres + "/wwwroot/img/" + resimAdi;

                    using var stream = new FileStream(kaydedilecekYer, FileMode.Create); // using maaliyet azaltmaya yarar
                    await model.Picture.CopyToAsync(stream);
                    user.PictureUrl = resimAdi;
                }

                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult HerkesErissin()
        {
            return View();
        }
    }
}
