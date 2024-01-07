using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Entities.Models;
using ProductManagement.WebUI.Areas.ManagementPanel.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagement.WebUI.Areas.ManagementPanel.Controllers {
    [Area("ManagementPanel")]
    public class AccountController : Controller {
        ProductManagementContext _context = new ProductManagementContext();
        public IActionResult Login() {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user == null) {
                ViewBag.Message = "Sen kimsin?";
                return View(model);
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Dashboard");
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login"); 
        }
    }
}
