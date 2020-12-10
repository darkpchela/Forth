using Forth.Identity.Entities;
using ForthSimple.Identity;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;
        private readonly IdentityContext identityContext;

        public IdentityService(IIdentityUnitOfWork identityUnitOfWork, IdentityContext identityContext)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.identityContext = identityContext;
        }

        public async Task<bool> SignIn(UserSignInVM userVm)
        {
            var res = await identityUnitOfWork.SignInManager.PasswordSignInAsync(userVm.Login, userVm.Password, false, false);
            if (res.Succeeded)
            {
                var user = identityContext.Users.FirstOrDefault(u => u.UserName == userVm.Login).LastOnlineDate = DateTime.Now;
                await identityContext.SaveChangesAsync();
            }
            return res.Succeeded;
        }

        public async Task SignOut()
        {
            await identityUnitOfWork.SignInManager.SignOutAsync();
        }

        public async Task<bool> SignUp(UserSignUpVM userVm)
        {
            var user = new User
            {
                Email = userVm.Email,
                UserName = userVm.Login,
                RegisterDate = DateTime.Now
            };
            var res = await identityUnitOfWork.UserManager.CreateAsync(user, userVm.Password);
            return res.Succeeded;
        }
    }
}