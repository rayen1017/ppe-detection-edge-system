using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPE.API.Data;

namespace PPE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var totalDetections = await _context.Detections.CountAsync();
            var totalViolations = await _context.Violations.CountAsync();
            var unresolvedViolations = await _context.Violations.CountAsync(v => !v.Resolved);

            var complianceRate = totalDetections == 0
                ? 100.0
                : Math.Round(100.0 * (totalDetections - totalViolations) / totalDetections, 1);

            return Ok(new
            {
                totalDetections,
                totalViolations,
                unresolvedViolations,
                complianceRate
            });
        }
    }
}