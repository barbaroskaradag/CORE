using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı Adı boş geçilemez.")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = "Şifre Tekrarı boş geçilemez.")]
        [Compare("Password",ErrorMessage ="Şifreler Uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        public string Name { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        public string Surname { get; set; }

        [Display(Name = "E- Mail")]
        [Required(ErrorMessage = "E-Mail boş geçilemez.")]
        public string Email { get; set; }
    }
}
