using CapybaraClickerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CapybaraClickerBackend.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Leaderboard> Leaderboards { get; set; }
    }
}
