using FluentValidation;
using JwtProje.Business.Contrete;
using JwtProje.Business.Interfaces;
using JwtProje.Business.ValidationRules.FluentValidation;
using JwtProje.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using JwtProje.DataAccess.Interfaces;
using JwtProje.Entities.Dtos.AppUserDtos;
using JwtProje.Entities.Dtos.ProductDtos;
using Microsoft.Extensions.DependencyInjection;

namespace JwtProje.Business.Containers.MicrosoftIoc
{
    public static class CustomExtension
    {
        public static void addDependenceis(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));

            services.AddScoped<IProductDal, EfProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IAppRoleDal, EfAppRoleRepository>();
            services.AddScoped<IAppRoleService, AppRoleManager>();

            services.AddScoped<IAppUserDal, EfAppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();

            services.AddScoped<IAppUserRoleDal, EfAppUserRoleRepository>();
            services.AddScoped<IAppUserRoleService, AppUserRoleManager>();

            services.AddTransient<IValidator<ProductAddDto>, ProductAddDtoValidator>();
            services.AddTransient<IValidator<ProductUpdateDto>, ProductUpdateDtoValidator>();
            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginDtoValidator>();
            services.AddTransient<IValidator<AppUserAddDto>, AppUserAddDtoValidator>();

            services.AddScoped<IJwtServices, JwtManager>();
        }
    }
}
