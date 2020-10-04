using FluentValidation.AspNetCore;
using JwtProje.Business.Containers.MicrosoftIoc;
using JwtProje.Business.Interfaces;
using JwtProje.Business.StringInfos;
using JwtProje.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using YSKProje.UdemyJwtProje.WebApi;

namespace JwtProje.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.addDependenceis();
            services.AddScoped(typeof(ValidId<>));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false; // https kullanýmý pasife çekilir
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = JwtInfo.Issuer,
                    ValidAudience = JwtInfo.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddControllers().AddFluentValidation(); // controlerýna fluent valýdation eklendi.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IAppUserService appUserService, IAppUserRoleService appUserRoleService, IAppRoleService appRoleService)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //bir kere çalýþmasý yeterli, sonrasýnda kapatýldý.
            JwtIdentityInitializer.Seed(appUserService, appUserRoleService, appRoleService).Wait();

            //hatayý global olarak ele alýr.
            //localhost/error
            app.UseExceptionHandler("/Error");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
