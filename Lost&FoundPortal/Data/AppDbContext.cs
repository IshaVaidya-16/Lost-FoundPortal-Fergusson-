using Lost_FoundPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace Lost_FoundPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Users { get; set; }
        public DbSet<LostItem> LostItems { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }
    }
}