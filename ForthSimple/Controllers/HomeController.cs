using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICookieIdentityService identityService;
        private readonly IMapper mapper;

        public HomeController(ICookieIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new UserSignUpVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpVM signUpVM)
        {
            if (!ModelState.IsValid)
                return View(signUpVM);
            var sinedUp = await identityService.SignUpAsync(signUpVM, HttpContext);
            if (!sinedUp)
            {
                ModelState.AddModelError("Register error", "User already exists");
                return View(signUpVM);
            }
            else
            {
                var signInVm = mapper.Map<UserSignInVM>(signUpVM);
                var signedIn = await identityService.SignInAsync(signInVm, HttpContext);
                if (signedIn)
                    return RedirectToAction(nameof(UserManagerController.Index), nameof(UserManagerController).Replace("Controller", ""));
                else
                    return RedirectToAction(nameof(SignIn));
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new UserSignInVM());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInVM signInVM)
        {
            if (!ModelState.IsValid)
                return View(signInVM);
            if (!await identityService.SignInAsync(signInVM, HttpContext))
            {
                ModelState.AddModelError("Authorize error", "Invalid login/password");
                return View(signInVM);
            }
            return RedirectToAction(nameof(UserManagerController.Index), nameof(UserManagerController).Replace("Controller", ""));
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await identityService.LogoutAsync(HttpContext);
            return RedirectToAction(nameof(SignIn));
        }
    }
}