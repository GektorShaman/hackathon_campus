using hackathon_campus.Core.Services;
using hackathon_campus.Core.Entities;
using hackathon_campus.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hackathon_campus.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserService userService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult List()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string userName)
        {
            if (userName == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(userName);
            var userViewModel = _userService.GetUserById(user.Id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "moderator")]
        public IActionResult Update(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddInformationForBot(string telegramFirstName)
        {
            _userService.AddTelegramIngormation(telegramFirstName);
            return RedirectToAction("Login", "Account");
        }



        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "moderator")]
        public async Task<IActionResult> UpdateConfirmed(UserViewModel newUser)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            if (newUser.Email == null)
            {
                return NotFound();
            }
            await _userService.UpdateUser(newUser);
            return RedirectToAction(nameof(Details), new { userName = newUser.NickName });
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }
    }
}