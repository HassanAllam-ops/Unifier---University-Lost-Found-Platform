using Unifier___University_Lost___Found_Platform.Models;
namespace Unifier___University_Lost___Found_Platform.Data
{
    public class StaticData
    {
        // ===== USERS =====
        public static List<User> Users = new List<User>
        {
            new User { Id = 1, FullName = "Ahmed Admin", Email = "admin@university.edu",
                       Password = "admin123", Role = "Admin", StudentId = "ADMIN01" },

            new User { Id = 2, FullName = "Ali Mohamed", Email = "ali@university.edu",
                       Password = "student123", Role = "Student", StudentId = "STU001" },

            new User { Id = 3, FullName = "Sara Ahmed", Email = "sara@university.edu",
                       Password = "student123", Role = "Student", StudentId = "STU002" },
        };

        // ===== LOST ITEMS =====
        public static List<LostItem> LostItems = new List<LostItem>
        {
            new LostItem { Id = 1, Title = "Laptop HP Black",
                           Description = "Black HP laptop with sticker on the back",
                           Category = "Electronics", Location = "Library - Floor 2",
                           DateReported = DateTime.Now.AddDays(-3),
                           Status = "Lost", ReportedByEmail = "ali@university.edu" },

            new LostItem { Id = 2, Title = "Student ID Card",
                           Description = "Student ID for Sara Ahmed - STU002",
                           Category = "ID & Cards", Location = "Cafeteria",
                           DateReported = DateTime.Now.AddDays(-1),
                           Status = "Found", ReportedByEmail = "sara@university.edu" },

            new LostItem { Id = 3, Title = "Blue Backpack",
                           Description = "Blue backpack with math books inside",
                           Category = "Bags", Location = "Building A - Room 101",
                           DateReported = DateTime.Now.AddDays(-5),
                           Status = "Claimed", ReportedByEmail = "ali@university.edu" },

            new LostItem { Id = 4, Title = "Car Keys",
                           Description = "Toyota car keys with red keychain",
                           Category = "Keys", Location = "Parking Lot",
                           DateReported = DateTime.Now.AddDays(-2),
                           Status = "Lost", ReportedByEmail = "sara@university.edu" },
        };

        // ===== HELPER METHODS =====

        // دوس على اليوزر بالايميل والباسورد
        public static User? Login(string email, string password)
        {
            return Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        // جيب كل الأيتمز
        public static List<LostItem> GetAllItems()
        {
            return LostItems.OrderByDescending(i => i.DateReported).ToList();
        }

        // ابحث بكلمة أو كاتيجوري
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

        // ضيف أيتم جديد
        public static void AddItem(LostItem item)
        {
            item.Id = LostItems.Count > 0 ? LostItems.Max(i => i.Id) + 1 : 1;
            item.DateReported = DateTime.Now;
            LostItems.Add(item);
        }

        // غير Status الأيتم
        public static void UpdateItemStatus(int id, string status)
        {
            var item = LostItems.FirstOrDefault(i => i.Id == id);
            if (item != null) item.Status = status;
        }

        // جيب الأيتمز بتاعت يوزر معين
        public static List<LostItem> GetItemsByUser(string email)
        {
            return LostItems.Where(i => i.ReportedByEmail == email).ToList();
        }
    }
}
