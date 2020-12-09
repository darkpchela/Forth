using ForthSimple.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return View(userManageService.GetAll());
        }
    }
}
