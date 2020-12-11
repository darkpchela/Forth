using ForthSimple.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IUserManageService
    {
        IEnumerable<UserVM> GetAllUsers();

        Task<bool> BlockUsersAsync(IEnumerable<int> ids);

        Task<bool> UnblockUsersAsync(IEnumerable<int> ids);

        Task<bool> DeleteUsersAsync(IEnumerable<int> ids);
    }
}