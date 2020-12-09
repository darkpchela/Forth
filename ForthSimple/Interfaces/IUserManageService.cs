using ForthSimple.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IUserManageService
    {
        IEnumerable<UserVM> GetAll();

        Task<bool> BlockUserAsync(int id);

        Task<bool> BlockUsersAsync(IEnumerable<int> ids);

        Task<bool> UnblockUserAsync(int id);

        Task<bool> UnblockUsersAsync(IEnumerable<int> ids);
    }
}