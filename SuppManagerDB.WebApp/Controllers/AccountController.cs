using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WebApp.Models;
using System.Security.Claims;

namespace SuppManagerDB.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // Login (GET) 
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginModel());
        }

        // Login (POST) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ЛОГІКА: Login повертає User або null
            var user = _authManager.Login(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login or password");
                return View(model);
            }

            // Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            };

            // Ролі (STRING, БЕЗ enum) 
            if (_authManager.HasPrivilege(user.UserID, "SupplierManager"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "SupplierManager"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // Forbidden 
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
