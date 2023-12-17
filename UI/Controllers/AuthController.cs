using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", "Please fill out the form completely.");
            }
            var result = _authService.Login(userForLoginDto);
            if (result.Success)
            {
                var claimsIdentiy = new ClaimsIdentity(result.Data, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return Redirect("/");
            }
            return View("Login", result.Message);
        }

        [Route("register")]
        public IActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel()
            {
                UserForRegister = new UserForRegisterDto()
            };
            return View(registerModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegister)
        {
            if (!ModelState.IsValid)
            {
                RegisterModel registerModel = new RegisterModel()
                {
                    UserForRegister = userForRegister,
                    Message = "Lütfen formu eksiksiz doldurunuz."
                };
                return View(registerModel);
            }
            var result = _authService.Register(userForRegister);
            if (result.Success)
            {
                var claimsIdentiy = new ClaimsIdentity(result.Data, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentiy);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return Redirect("/");
            }
            else
            {
                RegisterModel registerModel = new RegisterModel()
                {
                    UserForRegister = userForRegister,
                    Message = result.Message
                };
                return View(registerModel);
            }
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
