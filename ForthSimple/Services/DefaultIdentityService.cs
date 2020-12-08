using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class DefaultIdentityService : IUserIdentityService
    {
        private readonly ForthDbContext _dbContext;

        public DefaultIdentityService(ForthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LogoutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SignInAsync(UserSignInVM userVM, HttpContext httpContext)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userVM.Email && u.Password == userVM.Password);

            if (user is null)
                return false;

            var claims = new List<Claim>
            {
               new Claim(ClaimsIdentity.DefaultNameClaimType, userVM.Email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return true;
        }

        public async Task<bool> SignUpAsync(UserSignUpVM userVM, HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}