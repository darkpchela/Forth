using Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Interfaces
{
    internal interface IIdentityUnitOfWork
    {
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
    }
}