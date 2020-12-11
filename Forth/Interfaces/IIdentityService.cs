using ForthSimple.ViewModels;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignUp(UserSignUpVM userVm);

        Task<bool> SignIn(UserSignInVM userVm);

        Task SignOut();
    }
}