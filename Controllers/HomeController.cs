using CapybaraClickerBackend.Context;
using CapybaraClickerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapybaraClickerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly UserContext _userContext;

        public HomeController(UserContext userContext)
        {
            _userContext = userContext;
        }
        [HttpPost("addResult")]
        public ActionResult<string> SaveLeaderboardData([FromBody] LeaderboardDto leaderboard)
        {
            var usr = _userContext.Users.FirstOrDefault(x => x.Username == leaderboard.Username);

            if (usr == null)
            {
                return BadRequest("User not found.");
            }

            var lb = new Leaderboard()
            {
                Score = leaderboard.Score,
                User = usr
            };

            var leaderboradRow = _userContext.Leaderboards.FirstOrDefault(x => x.UserId == usr.UserId);
            if (leaderboradRow == null)
            {
                _userContext.Leaderboards.Add(lb);
            }
            else
            {
                _userContext.Remove<Leaderboard>(leaderboradRow);
                _userContext.Entry<Leaderboard>(leaderboradRow).CurrentValues.SetValues(lb);
            }

            _userContext.SaveChanges();

            return Ok("Leaderboards data was succesfully added\\updated");
        }

        [HttpGet("getTopPlayers")]
        public ActionResult<string[]> GetLeaderboardTop()
        {

            var querryResult = _userContext.Database
                .SqlQueryRaw<string>($"SELECT TOP 10\r\n\tUsername = usr.Username,\r\n\tScore = leader.Score\r\n\tfROM dbo.usr usr\r\n\tleft join dbo.Leaderboards leader on usr.UserId = leader.UserId\r\n\tOrder by leader.Score DESC").ToArray();

            if(querryResult == null)
            {
                return BadRequest("Cannot get top players");
            }

            return querryResult;
        }
    }
}
