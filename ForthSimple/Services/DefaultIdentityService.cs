using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Services
{
    public class DefaultIdentityService : IIdentityService
    {
        public Task<bool> LogoutAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignInAsync(UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignUpAsync(UserVM userVM)
        {
            throw new NotImplementedException();
        }
    }
}
