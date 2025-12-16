using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YourProjectNamespace.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("/auth/login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); 
        }

        [HttpPost("/auth/login")]
        [AllowAnonymous]
        public IActionResult Login(string email, string password, string? returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("/auth/google-login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin(string? returnUrl = "/")
        {
            var redirectUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";

            var props = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [HttpPost("/auth/logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}