namespace PPE.API.Models
{
    public class Detection
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string ClassName { get; set; } = string.Empty; // "head" ou "helmet"
        public float Confidence { get; set; }
        public float BboxX { get; set; }
        public float BboxY { get; set; }
        public float BboxWidth { get; set; }
        public float BboxHeight { get; set; }
        public string? ImageSnapshotPath { get; set; }
    }
}