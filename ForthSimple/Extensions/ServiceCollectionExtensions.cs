using AutoMapper;
using ForthSimple.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace ForthSimple.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services, params Assembly[] otherMapperAssemblies)
        {
            List<Assembly> assemblies = new List<Assembly>(otherMapperAssemblies)
            {
                Assembly.GetExecutingAssembly()
            };
            services.AddAutoMapper(assemblies);
            return services;
        }

        public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("Identity");
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddIdentity<IdentityUser, IdentityRole>(configs =>
            {
                configs.User.RequireUniqueEmail = true;
                configs.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false,
                    RequiredLength = 1
                };
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}