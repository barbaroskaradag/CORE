using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidator
{
    /// <summary>
    /// Verilen ingilizce mesajların türkçeleştirilmesi çin kullanılacak metotlar
    /// </summary>
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola minumum {length} karakter olmalıdır.",
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = $"Parola bir alfanümerik olmalıdır. (!,.,*, vs)",
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"İlgili kullanıcı adı ({userName}) sistemde kayıtlıdır.",
            };
        }

    }
}
