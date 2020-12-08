using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface ICookieIdentityService
    {
        Task<bool> SignUpAsync(UserSignUpVM userVM, HttpContext httpContext);

        Task<bool> SignInAsync(UserSignInVM userVM, HttpContext htttpContext);

        Task LogoutAsync(HttpContext httpContext);
    }
}