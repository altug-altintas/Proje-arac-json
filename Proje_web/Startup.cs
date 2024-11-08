using Proje_web.Models.AutoMappers;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Concrete;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Proje_Dal.Configuration;

namespace Proje_web
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
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost5173", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });


            services.AddDbContext<ProjectContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddIdentity<AppUser, IdentityRole>
                (
                    x =>
                    {
                        x.User.RequireUniqueEmail = true;    //ki�iye ait mail adresi e�siz olcak 
                        x.Password.RequiredLength = 4;    // �ifre karakter say�s�
                        x.Password.RequireLowercase = false;   // k���k harf zorunlulu�u olsun olmas�n
                        x.Password.RequireUppercase = false;   // b�y�k harf zorlunlu�u olsun olmasn
                        x.Password.RequireNonAlphanumeric = false;   // �ifrede �rne�in !, @, #, $, vb. gibi �zel karakterler olsun olmas�n
                        x.Password.RequireDigit = false;    // en az bir say� olsun olmasn
                        x.Password.RequiredUniqueChars = 0;

                        // katmanl� mimari  migration yaparken  package manager alan�nda  default proje   project context oldu�u yer start set projede  ba�lant� bilgilerinin oldu�u bu proje olmal�


                    }

                ).AddEntityFrameworkStores<ProjectContext>().AddDefaultTokenProviders();

            services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNamingPolicy = null; // C# ve JSON property isimlerinin e�le�mesi i�in
           });


            services.AddAutoMapper(typeof(Mappers));
            services.AddScoped<IAppUserRepo, AppUserRepo>();          
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));           
            services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
            services.AddScoped<IFirmaSahisRepo, FirmaSahisRepo>();
            services.AddScoped<IIslemRepo, IslemRepo>();
            services.AddScoped<IIslemDRepo, IslemDRepo>();
            services.AddScoped<IAracRepo, AracRepo>();





          //  services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;  //saklans�n,
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // farkl� algoritma d�n��ebilsin mi evet
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,  //al�nan token kendine has olsun ba�kalar�yla payla��lmas�n
                    ValidateIssuer = false    // al�nantoken hedefe y�nelik olsun
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseCors("AllowLocalhost5173");


            app.UseEndpoints(endpoints =>
            {

                //localhost/----areaname----/controllername/actionname/paramtere �eklilnde arealar olmal� 

                endpoints.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
