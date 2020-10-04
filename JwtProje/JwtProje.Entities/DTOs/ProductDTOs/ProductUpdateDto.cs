using System;
using System.Collections.Generic;
using System.Text;
using JwtProje.Entities.Interfaces;

namespace JwtProje.Entities.Dtos.ProductDtos
{
    public class ProductUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
