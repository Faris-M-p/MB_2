using Microsoft.EntityFrameworkCore;
namespace MB_2.Models.Entity
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<MB_2.Models.Entity.Employee> Employee { get; set; }
        public DbSet<MB_2.Models.Entity.Task> Task { get; set; }
        public DbSet<MB_2.Models.Entity.User> Users { get; set; }
    }
}
