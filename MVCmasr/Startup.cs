using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCmasr.Context;
using MVCmasr.Data.Repository;
using MVCmasr.Data.UnitOfWork;
using MVCmasr.Models;
using System;
using System.Security.Claims;

namespace MVCmasr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region Services
        public void ConfigureServices(IServiceCollection services)
        {
            #region Default Services
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddHttpContextAccessor();
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromSeconds(60);
            });

            services.AddMvc();
            #endregion

            #region DataBase
            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("MVCmasr"));
            });
            #endregion

            #region Register Custom Services
            services.AddScoped<ISongRepository , SongRepository >();
            services.AddScoped<IAlbumRepository , AlbumRepository >();
            services.AddScoped<IArtistRepository , ArtistRepository >();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IOrderItemRepository , OrderItemRepository>();
            services.AddScoped<IOrderRepository , OrderRepository>();
            services.AddScoped<IRolesRepository , RolesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Notification
            services.AddNotyf(config => { 
                config.DurationInSeconds = 10; 
                config.IsDismissable = true; 
                config.Position = NotyfPosition.TopRight; 
            });
            #endregion

            #region ASPIdentity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            #region Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminsOnly", policy => policy
                .RequireClaim(ClaimTypes.Role, "Admin"));
            });
            #endregion
        }
        #endregion

        #region Middlewares
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithRedirects("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseNotyf();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        #endregion
    }
}
