using MB_2.Models;
using MB_2.Models.User;
using MB_2.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MB_2.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public  UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(InputCreateUser input)
        {
            if (!ModelState.IsValid)
            {
               return View(input);
            }

            var result = await _userRepository.CreateUserAsync(input);

            if (!result.Status)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(input);
            }

            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public async Task<IActionResult> Login(InputLoginUser input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }
            var userDetails = await _userRepository.GetUserData(input.Email, input.Password);
            if (userDetails == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                ViewBag.Error = "Invalid email or password.";
                return View(input);
            }
            // Here you can add logic to verify the password and handle authentication
            // If successful, redirect to a different page or return a success response
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, userDetails.FK_User.ToString()),
              new Claim(ClaimTypes.Name, userDetails.Name),
              new Claim(ClaimTypes.Email, userDetails.Email)

            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                "CookieAuth",
                principal);


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");

            return RedirectToAction("Login", "User");
        }
    }
}
