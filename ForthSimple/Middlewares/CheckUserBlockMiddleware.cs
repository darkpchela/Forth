using ForthSimple.Interfaces;
using ForthSimple.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ForthSimple.Middlewares
{
    public class CheckUserBlockMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUserBlockMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, ForthDbContext dbContext, ICookieIdentityService authenticationService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string login = context.User.Identity.Name;
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == login);
                if (user != null && user.Blocked == true)
                {
                    await authenticationService.LogoutAsync(context);
                    context.Response.HttpContext.Items["Blocked"] = 1;
                    context.Response.Redirect("/Home/SignIn/");
                }
            }
            await _next.Invoke(context);
        }
    }
}