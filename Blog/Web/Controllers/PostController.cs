using Blog.Application.Interfaces;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string title, string content)
        {
            var post = await _postService.CreatePost(title, content);

            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _postService.GetPost(id);

            return Ok(post);
        }

        [HttpGet]
        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();

            return posts;
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(Guid id, string title, string content)
        {
            var post = await _postService.UpdatePost(id, title, content);

            return Ok(post);
        }
    }
}