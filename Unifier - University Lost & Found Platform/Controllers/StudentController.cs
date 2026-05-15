using Microsoft.AspNetCore.Mvc;
using Unifier___University_Lost___Found_Platform.Data;
using Unifier___University_Lost___Found_Platform.Models;

namespace Unifier___University_Lost___Found_Platform.Controllers
{
    public class StudentController : Controller
    {
        // GET: /Student/Dashboard
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "Student")
                return RedirectToAction("Dashboard", "Admin");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var myItems = StaticData.GetItemsByUser(email);

            ViewBag.MyLost = myItems.Count(i => i.Status == "Lost");
            ViewBag.MyFound = myItems.Count(i => i.Status == "Found");
            ViewBag.MyMatched = myItems.Count(i => i.Status == "Matched");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.RecentItems = myItems.Take(5).ToList();

            return View();
        }

        // GET: /Student/Report
        public IActionResult Report()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            if (HttpContext.Session.GetString("UserRole") != "Student")
                return RedirectToAction("Dashboard", "Admin");

            return View(new LostItem());
        }

        // POST: /Student/Report
        [HttpPost]
        public IActionResult Report(LostItem item)
        {
            if (!ModelState.IsValid)
                return View(item);

            // Email مجيش Auto من الـ Session
            item.ReportedByEmail = HttpContext.Session.GetString("UserEmail")!;
            StaticData.AddItem(item);

            TempData["Success"] = "Item reported successfully!";
            return RedirectToAction("MyItems");
        }

        // GET: /Student/MyItems
        public IActionResult MyItems()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var items = StaticData.GetItemsByUser(email);
            return View(items);
        }

        // GET: /Student/Details/5
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id && i.ReportedByEmail == email);

            if (item == null) return RedirectToAction("MyItems");
            return View(item);
        }

        // GET: /Student/Edit/5
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id && i.ReportedByEmail == email);

            if (item == null) return RedirectToAction("MyItems");
            return View(item);
        }

        // POST: /Student/Edit
        [HttpPost]
        public IActionResult Edit(LostItem item)
        {
            if (!ModelState.IsValid)
                return View(item);

            var existing = StaticData.LostItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null) return RedirectToAction("MyItems");

            existing.Title = item.Title;
            existing.Description = item.Description;
            existing.Category = item.Category;
            existing.Location = item.Location;
            existing.StudentId = item.StudentId;

            TempData["Success"] = "Item updated successfully!";
            return RedirectToAction("MyItems");
        }

        // GET: /Student/Delete/5
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id && i.ReportedByEmail == email);

            if (item == null) return RedirectToAction("MyItems");
            return View(item);
        }

        // POST: /Student/DeleteConfirmed
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail")!;
            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id && i.ReportedByEmail == email);

            if (item != null) StaticData.DeleteItem(id);

            TempData["Success"] = "Item deleted successfully!";
            return RedirectToAction("MyItems");
        }
    }
}
