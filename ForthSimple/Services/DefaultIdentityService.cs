using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class DefaultIdentityService : IUserIdentityService
    {
        public async Task<bool> LogoutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SignInAsync(UserSignInVM userVM)
        {
            return true;
        }

        public async Task<bool> SignUpAsync(UserSignUpVM userVM)
        {
            throw new NotImplementedException();
        }
    }
}
