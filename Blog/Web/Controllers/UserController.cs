using Blog.Application.Dto;
using Blog.Application.Interfaces;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IAuthService _authService;

        public UserController(UserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUser(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] LoginDTO loginData)
        {
            if (string.IsNullOrEmpty(loginData.Login) || string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("login and password are required.");
            }
            try
            {
                var user = await _userService.CreateUser(loginData.Login, loginData.Password);
                return Created("User successfully created", user);
            }
            catch (Exception ex)
            {
                return Conflict("login is already taken");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginData)
        {
            var user = await _userService.Login(loginData.Login, loginData.Password);

            return user != null ? Ok(_authService.CreateJwtToken(user)) : NotFound("User not found.");
        }
    }
}