using Identity.Entities;
using Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public IdentityUnitOfWork(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
    }
}
