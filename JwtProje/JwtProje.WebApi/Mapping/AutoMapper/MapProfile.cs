using AutoMapper;
using JwtProje.Entities.Concrete;
using JwtProje.Entities.Dtos.AppUserDtos;
using JwtProje.Entities.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtProje.WebApi.Mapping.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductAddDto>();

            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<AppUserAddDto, AppUser>();
            CreateMap<AppUser, AppUserAddDto>();
        }
    }
}
