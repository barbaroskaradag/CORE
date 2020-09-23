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
                //password ayarlar�.
                opt.Password.RequireDigit = false;      // say� olmas� zorunlulu�u yok
                opt.Password.RequireLowercase = false;  // k���k harf zorunlulu�u yok
                opt.Password.RequiredLength = 1;        // karakter uzunluk zorlunlulu�u k�salt�l�r. default 6
                //opt.Password.RequiredUniqueChars = 1; 
                opt.Password.RequireNonAlphanumeric = false; // nokta soru i�areti yok
                opt.Password.RequireUppercase = false;  // b�y�k harf zorunlulu�u yok

                //locked ayarlar�
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10); // lock s�resi, default 5 dk
                opt.Lockout.MaxFailedAccessAttempts = 3; // ka� defa yanl�� girilince kitlensin. Default 5 tir.

                //mail do�rulama
                //opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddPasswordValidator<CustomPasswordValidator>() // kendi yapt���m�z password kontrol
                .AddErrorDescriber<CustomIdentityValidator>() // identity s�n�f�na ait hatalar�n t�rk�ele�tiirlmesi
                .AddEntityFrameworkStores<UdemyContext>(); // database contesxtin ifade edilmesi. bizim olu�turdu�umuz user ve role g�re


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");
                opt.Cookie.HttpOnly = true; // true olursa cookie javascript taraf�ndan �ekilemez olur.
                opt.Cookie.Name = "AspNetCoreCookie"; // Cookie ad�
                opt.Cookie.SameSite = SameSiteMode.Strict; // lax -> cookie a�maya yarar, Strict -> sub domainler bile eri�emez
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // always -> http , sameasrequest -> https dahil
                opt.ExpireTimeSpan = TimeSpan.FromDays(20); // cookie ya�am s�resi

                //sayfa yetkiis olmayanlar� y�lendirme yapar.
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

            // Kullan�c� yetki ve kontrol kullan�lmas� 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
