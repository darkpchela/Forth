using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public AccountController(IMapper mapper, IIdentityService identityService)
        {
            this.mapper = mapper;
            this.identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            if (User.Identity.IsAuthenticated)
                await identityService.SignOut();

            return View(new UserSignUpVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM signUpVM)
        {
            if (!ModelState.IsValid)
                return View(signUpVM);
            var signedUp = await identityService.SignUp(signUpVM);
            if (!signedUp)
            {
                ModelState.AddModelError("Register error", "Login/Email already taken");
                return View(signUpVM);
            }
            else
            {
                var signInVm = mapper.Map<UserSignInVM>(signUpVM);
                var signedIn = await identityService.SignIn(signInVm);
                if (signedIn)
                    return RedirectToAction(nameof(UserManagerController.Index), nameof(UserManagerController).Replace("Controller", ""));
                else
                    return RedirectToAction(nameof(SignIn));
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            if (User.Identity.IsAuthenticated)
                await identityService.SignOut();
            return View(new UserSignInVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInVM signInVM)
        {
            if (!ModelState.IsValid)
                return View(signInVM);
            if (!await identityService.SignIn(signInVM))
            {
                ModelState.AddModelError("Authorize error", "Invalid login/password");
                return View(signInVM);
            }
            return RedirectToAction(nameof(UserManagerController.Index), nameof(UserManagerController).Replace("Controller", ""));
        }

        public IActionResult Exception()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await identityService.SignOut();
            return RedirectToAction(nameof(SignIn));
        }
    }
}