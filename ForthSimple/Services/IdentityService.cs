using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> SignUp(UserSignUpVM userVm)
        {
            var user = new IdentityUser { Email = userVm.Email, UserName = userVm.Email,/*, FirstName = userVm.FirstName, LastName = userVm.LastName */};
            var res = await identityUnitOfWork.UserManager.CreateAsync(user, userVm.Password);
            return res.Succeeded;
        }
    }
}