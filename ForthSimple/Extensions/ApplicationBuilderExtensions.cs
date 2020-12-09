using ForthSimple.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ForthSimple.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUserStatusValidator(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<UserStatusValidator>();
        }
    }
}