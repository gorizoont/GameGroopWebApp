using Microsoft.AspNetCore.Mvc;
using GameGroopWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using GameGroopWebApp.Models;
using GameGroopWebApp.Data;

namespace GameGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDBContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //User is found! Check Password.
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    //Password Correct. SingIn.
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Events");
                    }
                }
                //Password Incorrect.
                TempData["Error"] = "Wrong Password!";
                return View(loginViewModel);
            }
            //User not found.
            TempData["Error"] = "Wrong Email!";
            return View(loginViewModel);
        }
    }
}
