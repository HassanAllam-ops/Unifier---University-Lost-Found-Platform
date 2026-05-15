using Microsoft.AspNetCore.Mvc;
using Unifier___University_Lost___Found_Platform.Data;

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
                return RedirectToAction("Dashboard", "Student");

            ViewBag.LostItems = StaticData.GetLostItems();
            ViewBag.FoundItems = StaticData.GetFoundItems();
            ViewBag.TotalLost = StaticData.GetLostItems().Count;
            ViewBag.TotalFound = StaticData.GetFoundItems().Count;
            ViewBag.TotalMatched = StaticData.LostItems.Count(i => i.Status == "Matched");

            return View();
        }

        // GET: /Admin/AllItems
        public IActionResult AllItems()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Dashboard", "Student");

            ViewBag.AllItems = StaticData.GetAllItems();
            ViewBag.TotalLost = StaticData.LostItems.Count(i => i.Status == "Lost");
            ViewBag.TotalFound = StaticData.LostItems.Count(i => i.Status == "Found");
            ViewBag.TotalMatched = StaticData.LostItems.Count(i => i.Status == "Matched");
            ViewBag.TotalClaimed = StaticData.LostItems.Count(i => i.Status == "Claimed");
            return View();
        }

        // POST: /Admin/Match
        [HttpPost]
        public IActionResult Match(int lostId, int foundId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var success = StaticData.MatchItems(lostId, foundId);

            if (success)
                TempData["Success"] = "✅ Items matched successfully!";
            else
                TempData["Error"] = "❌ Match failed. Please try again.";

            return RedirectToAction("Dashboard");
        }
    }
}