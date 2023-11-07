using BCrypt.Net;
using CapybaraClickerBackend.Context;
using CapybaraClickerBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapybaraClickerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private UserContext _userContext;

        public AuthController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var usr = _userContext.Users.FirstOrDefault(x => x.Username == request.Username);

            if(usr != null)
            {
                return Conflict("Current username already use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            _userContext.Users.Add(user);
            _userContext.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            var usr = _userContext.Users.FirstOrDefault(x => x.Username == request.Username);

            if (usr == null)
            {
                return BadRequest("User not found.");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, usr.PasswordHash))
            {
                return BadRequest("Wrong password");
            }

            return Ok("User");
        }

        [HttpGet("check")]
        public ActionResult CheckBackendConnection()
        {
            return Ok("Server is up");
        }
    }
}
