using Microsoft.AspNetCore.Identity;
using System;

namespace ForthSimple.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        SignInManager<IdentityUser> SignInManager { get; }
        UserManager<IdentityUser> UserManager { get; }
    }
}