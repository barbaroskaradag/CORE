using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApiCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstWebApiCore
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
            services.AddScoped<IUserService, UserManager>();

            services.AddCors(cors =>
            {
                cors.AddPolicy("deneme",policy => 
                {
                    policy.AllowAnyOrigin() // b�t�n istekleri kontrol et demektir.
                    .AllowAnyHeader() //her t�rl� ba�l�k kabul edilebilir demektir.
                    .AllowAnyMethod(); // get post put i�in t�m istekleri kabul eder demektir.
                });
            });

            //services.AddCors(cors => 
            //{
            //    cors.AddPolicy("deneme", policy =>
            //    {
            //        policy.WithOrigins("http://karadagsunger.com.tr") // ge�erli adresin isteklerini kabul eder
            //        .AllowAnyHeader() //her t�rl� ba�l�k kabul edilebilir demektir.
            //        .AllowAnyMethod(); // get post put i�in t�m istekleri kabul eder demektir.
            //    });
            //});

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("deneme");

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}