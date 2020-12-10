using ForthSimple.Interfaces;
using ForthSimple.Identity.Entities;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityUnitOfWork identityUnitOfWork;
        public IdentityService(IIdentityUnitOfWork identityUnitOfWork)
        {
            this.identityUnitOfWork = identityUnitOfWork;
        }
        public async Task<bool> SignIn(UserSignInVM userVm)
        {
            var res = await identityUnitOfWork.SignInManager.PasswordSignInAsync(userVm.Email, userVm.Password, false, false);
            return res.Succeeded;
        }

        public async Task SignOut()
        {
            await identityUnitOfWork.SignInManager.SignOutAsync();
        }

        public async  Task<bool> SignUp(UserSignUpVM userVm)
        {
            var user = new User { Email = userVm.Email, UserName = userVm.Email,/*, FirstName = userVm.FirstName, LastName = userVm.LastName */};
            var res = await identityUnitOfWork.UserManager.CreateAsync(user, userVm.Password);
            return res.Succeeded;
        }
    }
}
