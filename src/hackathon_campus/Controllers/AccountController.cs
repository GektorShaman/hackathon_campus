using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using hackathon_campus.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using hackathon_campus.Core.Entities;

namespace hackathon_campus.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("Email,UserName,FirstName,LastName,Password,ConfirmPassword")] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Ошибка регистрации");
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, "user");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password,RememberMe")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User?.Identity?.IsAuthenticated == true)
                {
                    await _signInManager.SignOutAsync();
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user==null)
                {
                    ModelState.AddModelError(string.Empty, "Неверный адрес электронной почты");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.RememberMe, false);
                
                if (result.IsNotAllowed)
                {
                    return Unauthorized();
                }

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                ModelState.AddModelError(string.Empty, "Ошибка входа: неверные данные");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home"); ;
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
