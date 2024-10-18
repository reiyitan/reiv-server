using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace reiv_server.Models {
    public class ReivContext : IdentityDbContext<IdentityUser> {
        public ReivContext(DbContextOptions<ReivContext> options) : base(options) { }

        //entities
        public DbSet<Reiv> Reivs { get; set; }
    }
}
