using ForthSimple.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace ForthSimple.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        SignInManager<User> SignInManager { get; }
        UserManager<User> UserManager { get; }
    }
}