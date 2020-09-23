using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.CustomValidator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UdemyContext>();

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                //password ayarlarý.
                opt.Password.RequireDigit = false;      // sayý olmasý zorunluluðu yok
                opt.Password.RequireLowercase = false;  // küçük harf zorunluluðu yok
                opt.Password.RequiredLength = 1;        // karakter uzunluk zorlunluluðu kýsaltýlýr. default 6
                //opt.Password.RequiredUniqueChars = 1; 
                opt.Password.RequireNonAlphanumeric = false; // nokta soru iþareti yok
                opt.Password.RequireUppercase = false;  // büyük harf zorunluluðu yok

                //locked ayarlarý
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); // lock süresi, default 5 dk
                opt.Lockout.MaxFailedAccessAttempts = 3; // kaç defa yanlýþ girilince kitlensin. Default 5 tir.

                //mail doðrulama
                //opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddPasswordValidator<CustomPasswordValidator>() // kendi yaptýðýmýz password kontrol
                .AddErrorDescriber<CustomIdentityValidator>() // identity sýnýfýna ait hatalarýn türkçeleþtiirlmesi
                .AddEntityFrameworkStores<UdemyContext>(); // database contesxtin ifade edilmesi. bizim oluþturduðumuz user ve role göre


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");
                opt.Cookie.HttpOnly = true; // true olursa cookie javascript tarafýndan çekilemez olur.
                opt.Cookie.Name = "AspNetCoreCookie"; // Cookie adý
                opt.Cookie.SameSite = SameSiteMode.Strict; // lax -> cookie açmaya yarar, Strict -> sub domainler bile eriþemez
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // always -> http , sameasrequest -> https dahil
                opt.ExpireTimeSpan = TimeSpan.FromDays(20); // cookie yaþam süresi

                //sayfa yetkiis olmayanlarý yölendirme yapar.
                opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("FemalePolicy", cnf =>
                {
                    cnf.RequireClaim("gender", "female");
                });
            });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            // Kullanýcý yetki ve kontrol kullanýlmasý 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
