using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Dtos.AppUserDtos;

namespace JwtProje.Business.ValidationRules.FluentValidation
{
    public class AppUserAddDtoValidator : AbstractValidator<AppUserAddDto>
    {
        public AppUserAddDtoValidator()
        {
            RuleFor(I => I.UserName).NotEmpty().WithMessage("Kullanıcı Adı boş geçilemez");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez");
            RuleFor(I => I.FullName).NotEmpty().WithMessage("Ad Soyad alanı boş geçilemez");
        }
    }
}
