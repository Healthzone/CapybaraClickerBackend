using CapybaraClickerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CapybaraClickerBackend.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LeaderboardTopDto>(e =>
            {
                e.HasNoKey();
            });
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Leaderboard> Leaderboards { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.DefaultTypeMapping<LeaderboardTopDto>()
                .HasConversion<LeaderboardTopDto>();
            base.ConfigureConventions(configurationBuilder);

        }
    }
}
