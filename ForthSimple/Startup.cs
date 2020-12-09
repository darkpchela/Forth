using AutoMapper;
using ForthSimple.Extensions;
using ForthSimple.Interfaces;
using ForthSimple.Middlewares;
using ForthSimple.Models;
using ForthSimple.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForthSimple
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
            services.AddAuthentication();
            string connection = Configuration.GetConnectionString("Default");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn")
                );
            services.AddControllersWithViews();
            services.AddAutomapperProfiles();
            services.AddDbContext<ForthDbContext>(options => options.UseSqlServer(connection));
            services.AddTransient<ICookieBasedAuthenticationService, DefaultAuthenticationService>();
            services.AddTransient<IUserManageService, DefaultUserManageService>();
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
                app.UseExceptionHandler("/Account/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CheckUserBlockMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UserManager}/{action=Index}");
            });
        }
    }
}