using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Unifier___University_Lost___Found_Platform.Models;
using Unifier___University_Lost___Found_Platform.Data;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // لو مش logged in يروح للوجين
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var items = StaticData.GetAllItems();
            ViewBag.TotalLost = items.Count(i => i.Status == "Lost");
            ViewBag.TotalFound = items.Count(i => i.Status == "Found");
            ViewBag.TotalClaimed = items.Count(i => i.Status == "Claimed");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            ViewBag.RecentItems = items.Take(5).ToList();

            return View();
        }
    }
}
