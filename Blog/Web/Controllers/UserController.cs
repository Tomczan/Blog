using Blog.Application.Interfaces;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUser(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string name, string password)
        {
            return Created("", await _userService.CreateUser(name, password));
        }

        [HttpGet("{name}, {password}")]
        public async Task<IActionResult> Login(string name, string password)
        {
            return Ok(await _userService.Login(name, password));
        }
    }
}