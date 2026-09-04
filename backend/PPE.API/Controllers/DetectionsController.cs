using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPE.API.Data;
using PPE.API.Models;
using Microsoft.AspNetCore.SignalR;
using PPE.API.Hubs;

namespace PPE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<DetectionHub> _hubContext;


        public DetectionsController(AppDbContext context , IHubContext<DetectionHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [HttpPost]
public async Task<IActionResult> PostDetection([FromBody] Detection detection)
{
    detection.Timestamp = DateTime.UtcNow;
    _context.Detections.Add(detection);
    await _context.SaveChangesAsync();

    
    if (detection.ClassName == "head" && detection.Confidence >= 0.6f)
    {
        var violation = new Violation
        {
            DetectionId = detection.Id,
            AlertSent = false,
            Resolved = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Violations.Add(violation);
        await _context.SaveChangesAsync();
    }
    await _hubContext.Clients.All.SendAsync("ReceiveDetection", detection);
    return Ok(detection);
}


        [HttpGet]
        public async Task<IActionResult> GetDetections()
        {
            var detections = await _context.Detections
                .OrderByDescending(d => d.Timestamp)
                .Take(50)
                .ToListAsync();

            return Ok(detections);
        }
    }
}