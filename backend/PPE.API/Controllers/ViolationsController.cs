using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPE.API.Data;
using PPE.API.Models;

namespace PPE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViolationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ViolationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetViolations()
        {
            var violations = await _context.Violations
                .Include(v => v.Detection)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();

            return Ok(violations);
        }

        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> ResolveViolation(int id)
        {
            var violation = await _context.Violations.FindAsync(id);
            if (violation == null) return NotFound();

            violation.Resolved = true;
            await _context.SaveChangesAsync();

            return Ok(violation);
        }
    }
}