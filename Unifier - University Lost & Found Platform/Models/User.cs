namespace Unifier___University_Lost___Found_Platform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = ""; // "Student", "Admin"
        public string StudentId { get; set; } = "";
    }
}
