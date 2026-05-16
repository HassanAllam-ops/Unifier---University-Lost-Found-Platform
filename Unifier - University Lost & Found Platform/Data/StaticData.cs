using Unifier___University_Lost___Found_Platform.Models;
namespace Unifier___University_Lost___Found_Platform.Data
{
    public static class StaticData
    {
        // ===== USERS =====
        public static List<User> Users = new List<User>
        {
            new User { Id = 1, FullName = "Ahmed Admin", Email = "admin@university.edu",
                       Password = "admin123", Role = "Admin", StudentId = "ADMIN01" },

            new User { Id = 2, FullName = "Hassan Allam", Email = "Hassan@university.edu",
                       Password = "Allam18122022", Role = "Student", StudentId = "STU001" },

            new User { Id = 3, FullName = "Sara Ahmed", Email = "Sara@university.edu",
                       Password = "student123", Role = "Student", StudentId = "STU002" },
        };

        // ===== LOST ITEMS =====
        public static List<LostItem> LostItems = new List<LostItem>
        {
            new LostItem { Id = 1, Title = "Laptop HP Black",
                           Description = "Black HP laptop with sticker on the back",
                           Category = "Electronics", Location = "Library - Floor 2",
                           DateReported = DateTime.Now.AddDays(-3),
                           Status = "Lost", ReportedByEmail = "hassan@university.edu",
                           StudentId = "STU001" },

            new LostItem { Id = 2, Title = "Student ID Card",
                           Description = "Student ID for Sara Ahmed",
                           Category = "ID & Cards", Location = "Cafeteria",
                           DateReported = DateTime.Now.AddDays(-1),
                           Status = "Found", ReportedByEmail = "sara@university.edu",
                           StudentId = "STU002" },

            new LostItem { Id = 3, Title = "Blue Backpack",
                           Description = "Blue backpack with math books inside",
                           Category = "Bags", Location = "Building A - Room 101",
                           DateReported = DateTime.Now.AddDays(-5),
                           Status = "Lost", ReportedByEmail = "sara@university.edu",
                           StudentId = "STU002" },

            new LostItem { Id = 4, Title = "Car Keys",
                           Description = "Toyota car keys with red keychain",
                           Category = "Keys", Location = "Parking Lot",
                           DateReported = DateTime.Now.AddDays(-2),
                           Status = "Found", ReportedByEmail = "hassan@university.edu",
                           StudentId = "STU001" },
        };

        // ===== HELPER METHODS =====

        public static User? Login(string email, string password)
        {
            return Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public static List<LostItem> GetAllItems()
        {
            return LostItems.OrderByDescending(i => i.DateReported).ToList();
        }

        public static List<LostItem> GetLostItems()
        {
            return LostItems.Where(i => i.Status == "Lost")
                            .OrderByDescending(i => i.DateReported).ToList();
        }

        public static List<LostItem> GetFoundItems()
        {
            return LostItems.Where(i => i.Status == "Found")
                            .OrderByDescending(i => i.DateReported).ToList();
        }

        public static List<LostItem> SearchItems(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return GetAllItems();
            return LostItems.Where(i =>
                i.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                i.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                i.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                i.Location.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        public static void AddItem(LostItem item)
        {
            item.Id = LostItems.Count > 0 ? LostItems.Max(i => i.Id) + 1 : 1;
            item.DateReported = DateTime.Now;
            LostItems.Add(item);
        }

        public static List<LostItem> GetItemsByUser(string email)
        {
            return LostItems.Where(i => i.ReportedByEmail == email)
                            .OrderByDescending(i => i.DateReported).ToList();
        }

        public static void DeleteItem(int id)
        {
            var item = LostItems.FirstOrDefault(i => i.Id == id);
            if (item != null) LostItems.Remove(item);
        }

        // ===== MATCH LOGIC =====
        public static bool MatchItems(int lostId, int foundId)
        {
            var lostItem = LostItems.FirstOrDefault(i => i.Id == lostId && i.Status == "Lost");
            var foundItem = LostItems.FirstOrDefault(i => i.Id == foundId && i.Status == "Found");

            if (lostItem == null || foundItem == null) return false;

            // غير Status بتاعهم
            lostItem.Status = "Matched";
            lostItem.MatchedWithId = foundId;

            foundItem.Status = "Claimed";
            foundItem.MatchedWithId = lostId;

            return true;
        }
    }
}