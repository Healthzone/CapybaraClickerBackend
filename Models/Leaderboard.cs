using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapybaraClickerBackend.Models
{
    public class Leaderboard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Score { get; set; }

        public long UserId { get; set; }
        public User User { get; set; } = null;
}
}
