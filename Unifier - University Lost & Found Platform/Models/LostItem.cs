namespace Unifier___University_Lost___Found_Platform.Models
{
    public class LostItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime DateReported { get; set; }
        public string Status { get; set; } = ""; // "Lost", "Found", "Matched", "Claimed"
        public string ReportedByEmail { get; set; } = "";
        public string StudentId { get; set; } = "";
        public int? MatchedWithId { get; set; } = null; // ID الأيتم اللي اتعمل معاه Match
    }
}