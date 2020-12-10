using ForthSimple.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IUserManageService
    {
        IEnumerable<UserVM> GetAll();

        Task<bool> BlockUsersAsync(IEnumerable<string> ids);

        Task<bool> UnblockUsersAsync(IEnumerable<string> ids);

        Task<bool> DeleteUsersAsync(IEnumerable<string> ids);
    }
}