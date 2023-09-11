using Blog.Application.Interfaces;
using Blog.Infrastructure.Services;
using Blog.Web.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;
        private readonly IAuthService _authService;

        public UserController(IUserRepository userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("login and password are required.");
            }
            try
            {
                var user = await _userService.CreateAsync(request.Login, request.Password);
                return Created("User successfully created", user);
            }
            catch (Exception ex)
            {
                return Conflict("login is already taken");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _userService.LoginAsync(request.Login, request.Password);

            return user != null ? Ok(_authService.CreateJwtToken(user)) : NotFound("User not found.");
        }
    }
}