using Microsoft.AspNetCore.Mvc;
using Unifier___University_Lost___Found_Platform.Data;
using Unifier___University_Lost___Found_Platform.Models;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserEmail") != null)
            {
                var role = HttpContext.Session.GetString("UserRole");
                return role == "Admin"
                    ? RedirectToAction("Dashboard", "Admin")
                    : RedirectToAction("Dashboard", "Student");
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = StaticData.Login(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // كل واحد يروح على Dashboard بتاعه
            return user.Role == "Admin"
                ? RedirectToAction("Dashboard", "Admin")
                : RedirectToAction("Dashboard", "Student");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}