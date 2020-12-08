using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class DefaultIdentityService : ICookieIdentityService
    {
        private readonly ForthDbContext dbContext;
        private readonly IMapper mapper;

        public DefaultIdentityService(ForthDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> SignInAsync(UserSignInVM userVM, HttpContext httpContext)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userVM.Email && u.Password == userVM.Password);
            if (user is null)
                return false;
            await Authenticate(httpContext, userVM.Email);
            return true;
        }

        public async Task<bool> SignUpAsync(UserSignUpVM userVM, HttpContext httpContext)
        {
            var user = mapper.Map<User>(userVM);
            if (await dbContext.Users.AnyAsync(u => u.Email == userVM.Email))
                return false;
            await dbContext.Users.AddAsync(user);
            dbContext.SaveChanges();
            await Authenticate(httpContext, user.Email);
            return true;
        }

        private async Task Authenticate(HttpContext httpContext, string login)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}