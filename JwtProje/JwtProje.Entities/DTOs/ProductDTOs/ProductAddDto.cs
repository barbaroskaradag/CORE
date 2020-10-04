using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Interfaces;

namespace JwtProje.Entities.Dtos.ProductDtos
{
    public class ProductAddDto : IDto
    {
        public string Name { get; set; }
    }
}
