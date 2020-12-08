using ForthSimple.ViewModels;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignUpAsync(UserVM userVM);

        Task<bool> SignInAsync(UserVM userVM);

        Task<bool> LogoutAsync(int id);
    }
}