using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.Models;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using iden = ForthSimple.Identity.Entities;

namespace ForthSimple.Services
{
    public class DefaultAuthenticationService : ICookieBasedAuthenticationService
    {
        private readonly ForthDbContext dbContext;
        private readonly IMapper mapper;

        public DefaultAuthenticationService(ForthDbContext dbContext, IMapper mapper)
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
            user.LastOnlineDate = DateTime.Now;
            await dbContext.SaveChangesAsync();
            await Authenticate(httpContext, userVM.Email);
            return true;
        }

        public async Task<bool> SignUpAsync(UserSignUpVM userVM, HttpContext httpContext)
        {
            var user = mapper.Map<User>(userVM);
            if (await dbContext.Users.AnyAsync(u => u.Email == userVM.Email))
                return false;
            user.RegisterDate = DateTime.Now;
            await dbContext.Users.AddAsync(user);
            var iUser = new iden.User {Email=userVM.Email, UserName = userVM.Email };
            await dbContext.SaveChangesAsync();
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