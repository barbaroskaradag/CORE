using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeClint.Models
{
    public class AppUserLogin
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola gereklidir")]
        public string Password { get; set; }
    }
}
