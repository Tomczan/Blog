using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string title, string content, string authorId)
        {
            return Created("", await _postService.CreatePost(title, content, authorId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _postService.GetPostById(id);

            return Ok(post);
        }

        [HttpGet]
        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();

            return posts;
        }

        [HttpGet("search/{title}")]
        public async Task<List<Post>> GetPostsByTitle(string title)
        {
            var posts = await _postService.GetPostByTitle(title);

            return posts;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(string id, string title, string content, string authorId)
        {
            var post = await _postService.UpdatePost(id, title, content, authorId);

            return Ok(post);
        }

        [HttpDelete]
        public IActionResult DeletePost(string id)
        {
            _postService.DeletePost(id);

            return Ok(id);
        }
    }
}