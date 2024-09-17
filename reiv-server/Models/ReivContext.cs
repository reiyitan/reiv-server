using Microsoft.EntityFrameworkCore;

namespace reiv_server.Models {
    public class ReivContext : DbContext {
        public ReivContext(DbContextOptions<ReivContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!; 
    }
}
