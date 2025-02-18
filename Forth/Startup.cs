using ForthSimple.Extensions;
using ForthSimple.Interfaces;
using ForthSimple.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn")
                );
            services.AddControllersWithViews();
            services.AddAutomapperProfiles();
            services.AddTransient<IUserManageService, UserManageService>();
            services.AddIdentityContext(Configuration);
            services.AddTransient<IIdentityUnitOfWork, IdentityUnitOfWork>();
            services.AddTransient<IIdentityService, IdentityService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Exception");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseUserStatusValidator();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UserManager}/{action=Index}");
                endpoints.MapFallbackToController("SignIn", "Account");
            });
        }
    }
}