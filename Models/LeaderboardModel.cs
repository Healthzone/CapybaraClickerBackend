namespace CapybaraClickerBackend.Models
{
    public class LeaderboardModel
    {
        public LeaderboardModel()
        {
            Users = new List<LeaderboardTopUser>();
        }
        public LeaderboardModel(IList<LeaderboardTopUser> users)
        {
            Users = users;
        }
        public IList<LeaderboardTopUser> Users { get; set; }
    }
}
