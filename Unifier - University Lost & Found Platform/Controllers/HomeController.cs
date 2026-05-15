using Microsoft.AspNetCore.Mvc;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin"
                ? RedirectToAction("Dashboard", "Admin")
                : RedirectToAction("Dashboard", "Student");
        }
    }
}