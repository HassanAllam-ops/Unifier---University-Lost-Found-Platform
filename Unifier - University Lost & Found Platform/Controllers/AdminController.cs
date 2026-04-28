using Unifier___University_Lost___Found_Platform.Data;
using Microsoft.AspNetCore.Mvc;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/Dashboard
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Index", "Home");

            var items = StaticData.GetAllItems();
            ViewBag.TotalLost = items.Count(i => i.Status == "Lost");
            ViewBag.TotalFound = items.Count(i => i.Status == "Found");
            ViewBag.TotalClaimed = items.Count(i => i.Status == "Claimed");
            return View(items);
        }

        // POST: /Admin/UpdateStatus
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Index", "Home");

            StaticData.UpdateItemStatus(id, status);
            return RedirectToAction("Dashboard");
        }
    }
}
