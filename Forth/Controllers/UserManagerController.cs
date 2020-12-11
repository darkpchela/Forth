using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    [Authorize]
    public class UserManagerController : Controller
    {
        private readonly IUserManageService userManageService;

        public UserManagerController(IUserManageService userManageService)
        {
            this.userManageService = userManageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = (List<UserVM>)userManageService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block(IEnumerable<UserVM> usersVM)
        {
            var ids = GetUsersId(usersVM);
            var res = await userManageService.BlockUsersAsync(ids);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(IEnumerable<UserVM> usersVM)
        {
            var ids = GetUsersId(usersVM);
            var res = await userManageService.UnblockUsersAsync(ids);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<UserVM> usersVM)
        {
            var ids = GetUsersId(usersVM);
            var res = await userManageService.DeleteUsersAsync(ids);
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private IEnumerable<int> GetUsersId(IEnumerable<UserVM> usersVM)
        {
            var ids = usersVM.Where(u => u.Selected).Select(u => u.Id);
            return ids;
        }
    }
}