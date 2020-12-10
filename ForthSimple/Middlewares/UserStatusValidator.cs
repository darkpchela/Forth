using ForthSimple.Identity;
using ForthSimple.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ForthSimple.Middlewares
{
    public class UserStatusValidator
    {
        private readonly RequestDelegate _next;

        public UserStatusValidator(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IdentityContext dbContext, ICookieBasedAuthenticationService authenticationService)
        {
            //if (context.User.Identity.IsAuthenticated)
            //{
            //    string login = context.User.Identity.Name;
            //    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == login);
            //    if (user == null || user.Blocked == true)
            //    {
            //        await authenticationService.LogoutAsync(context);
            //        context.Response.Redirect("/Account/Exception");
            //    }
            //}
            await _next.Invoke(context);
        }
    }
}