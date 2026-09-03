using Microsoft.EntityFrameworkCore;
using PPE.API.Models;

namespace PPE.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Detection> Detections { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<Camera> Cameras { get; set; }
    }
}