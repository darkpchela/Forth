using AutoMapper;
using ForthSimple.Interfaces;
using ForthSimple.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForthSimple.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICookieBasedAuthenticationService authenticationService;
        private readonly IMapper mapper;

        public AccountController(ICookieBasedAuthenticationService authenticationService, IMapper mapper)
        {
            this.authenticationService = authenticationService;
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
            var sinedUp = await authenticationService.SignUpAsync(signUpVM, HttpContext);
            if (!sinedUp)
            {
                ModelState.AddModelError("Register error", "User already exists");
                return View(signUpVM);
            }
            else
            {
                var signInVm = mapper.Map<UserSignInVM>(signUpVM);
                var signedIn = await authenticationService.SignInAsync(signInVm, HttpContext);
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
            if (!await authenticationService.SignInAsync(signInVM, HttpContext))
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
            await authenticationService.LogoutAsync(HttpContext);
            return RedirectToAction(nameof(SignIn));
        }
    }
}