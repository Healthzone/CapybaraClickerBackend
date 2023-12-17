using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapybaraClickerBackend.Models
{
    [Keyless]
    public class LeaderboardTopUser
    {
        [Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Column("Score")]
        public long Score { get; set; }

    }
}
