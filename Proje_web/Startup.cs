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

            // CORS Policy tanımı
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost5173", builder =>
                {
                    builder.WithOrigins("http://localhost:5173") // Buraya izin vermek istediğiniz origin'leri ekleyin
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials(); // Cookie tabanlı oturumlar için gerekli
                });
            });

            // Veritabanı ve kimlik doğrulama servisleri
            services.AddDbContext<ProjectContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<ProjectContext>()
            .AddDefaultTokenProviders();

            // JWT Ayarları
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
                });

            services.AddAuthorization();

            // AutoMapper ve Repository'ler
            services.AddAutoMapper(typeof(Mappers));
            services.AddScoped<IAppUserRepo, AppUserRepo>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();
            services.AddScoped<IFirmaSahisRepo, FirmaSahisRepo>();
            services.AddScoped<IIslemRepo, IslemRepo>();
            services.AddScoped<IIslemDRepo, IslemDRepo>();
            services.AddScoped<IAracRepo, AracRepo>();
            services.AddScoped<IisLemNewRepo, isLemNewRepo>();

            // JSON Ayarları
            services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
                   options.JsonSerializerOptions.IgnoreNullValues = true;
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

            // CORS Middleware sırası
            app.UseCors("AllowLocalhost5173");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
