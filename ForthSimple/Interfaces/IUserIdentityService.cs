using ForthSimple.ViewModels;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IUserIdentityService
    {
        Task<bool> SignUpAsync(UserSignUpVM userVM);

        Task<bool> SignInAsync(UserSignInVM userVM);

        Task<bool> LogoutAsync(int id);
    }
}