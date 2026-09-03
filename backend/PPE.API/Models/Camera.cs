namespace PPE.API.Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "active"; // "active" ou "offline"
        public DateTime LastSeenAt { get; set; }
    }
}