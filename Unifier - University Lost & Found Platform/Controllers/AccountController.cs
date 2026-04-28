using Unifier___University_Lost___Found_Platform.Models;
using Unifier___University_Lost___Found_Platform.Data;
using Microsoft.AspNetCore.Mvc;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class AccountController: Controller
    {
        // GET: /Account/Login
        public IActionResult Login()
        {
            // لو بالفعل logged in يروح للهوم
            if (HttpContext.Session.GetString("UserEmail") != null)
                return RedirectToAction("Index", "Home");

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

            // حفظ بيانات اليوزر في Session
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
