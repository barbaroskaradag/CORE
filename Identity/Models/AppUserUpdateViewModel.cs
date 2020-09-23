using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class AppUserUpdateViewModel
    {
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Name { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        public string SurName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir mail adresi giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon No")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Resim")]
        public IFormFile Picture { get; set; }

        public string PictureUrl { get; set; }
    }
}
