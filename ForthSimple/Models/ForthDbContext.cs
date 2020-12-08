using Microsoft.EntityFrameworkCore;

namespace ForthSimple.Models
{
    public class ForthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ForthDbContext(DbContextOptions<ForthDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}