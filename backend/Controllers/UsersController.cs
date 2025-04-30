using EnsolversChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnsolversChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service) => _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CredentialsDto creds)
        {
            try
            {
                var user = await _service.Register(creds.Username, creds.Password);
                return CreatedAtAction(nameof(GetNotes), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsDto creds)
        {
            var user = await _service.Authenticate(creds.Username, creds.Password);
            return user != null ? Ok(user) : Unauthorized();
        }

        [HttpGet("{id}/notes")]
        public async Task<IActionResult> GetNotes(int id)
        {
            var notes = await _service.GetUserNotes(id);
            return Ok(notes);
        }
    }

    public class CredentialsDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
