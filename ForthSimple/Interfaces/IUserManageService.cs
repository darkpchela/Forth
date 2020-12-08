using ForthSimple.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForthSimple.Interfaces
{
    public interface IUserManageService
    {
        Task<UserVM> GetUserAsync(int id);

        Task<IEnumerable<UserVM>> GetAllAsync();

        Task<bool> BlockUserAsync(int id);

        Task<bool> BlockUsersAsync(IEnumerable<int> ids);

        Task<bool> UnblockUser(int id);

        Task<bool> UnblockUserAsync(IEnumerable<int> ids);
    }
}