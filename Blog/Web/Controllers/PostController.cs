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
        public Task<IActionResult> CreatePost(Post post)
        {
            _postService.CreatePost(post.Title, post.Content);

            return Task.FromResult<IActionResult>(CreatedAtAction(nameof(GetPost), new { id = post.Id }, post));
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetPost(Guid id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
                return Task.FromResult<IActionResult>(NotFound());

            return Task.FromResult<IActionResult>(Ok(post));
        }
    }
}