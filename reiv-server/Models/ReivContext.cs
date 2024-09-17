using Microsoft.EntityFrameworkCore;

namespace reiv_server.Models {
    public class ReivContext : DbContext {
        public ReivContext(DbContextOptions<ReivContext> options) : base(options) { }

        public DbSet<Reiv> Reivs { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!; 
    }
}
