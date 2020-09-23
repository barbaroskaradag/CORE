using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [AutoValidateAntiforgeryToken] // post işlemlerine otomatik kontrol ekler
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // client üzerinden bir istek var ise token kontrolü yapar. Mevcut sayfa üzerinde token bulunmaktadır.
        public async Task<IActionResult> GirisYap(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                /*
                 parametre değerleri
                 1 - Kullanıcı adı
                 2 - Şifre
                 3 - Beni hatırla
                 4 - Lock olup/olmaması
                 */
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                else if (result.IsLockedOut) // hesap kitli ise
                {
                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));
                    var kisitlananSure = gelen.Value;
                    var kalanDakika = kisitlananSure.AddMinutes((-1) * DateTime.Now.Minute);
                    ModelState.AddModelError("", $"3 Kere yanlış girdiğiniz için hesabınız kitlenmiştir. {kalanDakika} dk sonra tekrar deneyebilirsiniz.");
                }
                else if (result.IsNotAllowed) // mail adresi doğrulama yapılmamışsa
                {
                    ModelState.AddModelError("", "Email adresinizi lütfen doğrulayınız.");
                }
                else
                {
                    var yanlisGirilmeSayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));
                    ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı. {5- yanlisGirilmeSayisi} kadar yanlış girerseniz hesabınız kitlenecektir.");
                }
            }

            return View("Index", model);
        }

        public IActionResult KayitOl()
        {
            return View(new UserSignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> KayitOl(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.UserName,
                    UserName = model.UserName,
                };

                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
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


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
