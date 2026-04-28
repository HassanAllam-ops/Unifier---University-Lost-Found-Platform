using Microsoft.AspNetCore.Mvc;
using Unifier___University_Lost___Found_Platform.Data;
using Unifier___University_Lost___Found_Platform.Models;

namespace Unifier.Controllers
{
    public class ItemsController : Controller
    {
        // GET: /Items/Search
        public IActionResult Search(string keyword = "")
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var results = StaticData.SearchItems(keyword);
            ViewBag.Keyword = keyword;
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(results);
        }

        // GET: /Items/Report
        public IActionResult Report()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            return View(new LostItem());
        }

        // POST: /Items/Report
        [HttpPost]
        public IActionResult Report(LostItem item)
        {
            if (!ModelState.IsValid)
                return View(item);

            item.ReportedByEmail = HttpContext.Session.GetString("UserEmail")!;
            StaticData.AddItem(item);

            TempData["Success"] = "Item reported successfully!";
            return RedirectToAction("Search");
        }

        // GET: /Items/Details/5
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /Items/Edit/5
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            // Student يعدل بسه الأيتمز بتاعته
            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && item.ReportedByEmail != email)
                return RedirectToAction("Search");

            return View(item);
        }

        // POST: /Items/Edit/5
        [HttpPost]
        public IActionResult Edit(LostItem item)
        {
            if (!ModelState.IsValid)
                return View(item);

            var existing = StaticData.LostItems.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null) return NotFound();

            existing.Title = item.Title;
            existing.Description = item.Description;
            existing.Category = item.Category;
            existing.Location = item.Location;
            existing.Status = item.Status;

            TempData["Success"] = "Item updated successfully!";
            return RedirectToAction("MyItems");
        }

        // GET: /Items/Delete/5
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin" && item.ReportedByEmail != email)
                return RedirectToAction("Search");

            return View(item);
        }

        // POST: /Items/DeleteConfirmed
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = StaticData.LostItems.FirstOrDefault(i => i.Id == id);
            if (item != null) StaticData.LostItems.Remove(item);

            TempData["Success"] = "Item deleted successfully!";
            return RedirectToAction("MyItems");
        }

        // GET: /Items/MyItems
        public IActionResult MyItems()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("Login", "Account");

            var email = HttpContext.Session.GetString("UserEmail")!;
            var items = StaticData.GetItemsByUser(email);
            return View(items);
        }
    }
}