namespace PPE.API.Models
{
    public class Violation
    {
        public int Id { get; set; }
        public int DetectionId { get; set; }
        public Detection? Detection { get; set; }
        public bool AlertSent { get; set; }
        public bool Resolved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}