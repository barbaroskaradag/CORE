using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Dtos.ProductDtos;

namespace JwtProje.Business.ValidationRules.FluentValidation
{
    public class ProductAddDtoValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddDtoValidator()
        {
            RuleFor(I => I.Name).NotEmpty().WithMessage("ad alanı boş geçilemez");
        }
    }
}
