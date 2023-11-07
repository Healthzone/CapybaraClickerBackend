using CapybaraClickerBackend.Context;
using CapybaraClickerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
                _userContext.Remove(leaderboradRow);
                _userContext.Add(lb);
            }

            _userContext.SaveChanges();

            return Ok("Leaderboards data was successful added\\updated");
        }

        [HttpGet("getTopPlayers")]
        public ActionResult<string> GetLeaderboardTop()
        {

            var querryResult = _userContext.Database
                .SqlQuery<LeaderboardTopDto>($"exec [dbo].[GetTop10Players]")?.ToList();

            if(querryResult == null)
            {
                return BadRequest("Cannot get top players");
            }

            return JsonSerializer.Serialize(querryResult, new JsonSerializerOptions { WriteIndented = true}); ;
        }
    }
}
