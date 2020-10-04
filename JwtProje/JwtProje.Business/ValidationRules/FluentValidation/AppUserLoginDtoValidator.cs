using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Dtos.AppUserDtos;

namespace JwtProje.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(I => I.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
        }
    }
}
