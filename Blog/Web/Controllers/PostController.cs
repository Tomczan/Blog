using Blog.Application.Dtos;
using Blog.Application.Posts.Commands;
using Blog.Application.Posts.Queries;
using Blog.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IMediator _mediator;

        public PostController(PostService postService, IMediator mediator)
        {
            _postService = postService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryParamsDTO postParams)
        {
            var query = new GetPostsQuery(postParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("my-posts/")]
        [Authorize]
        public async Task<IActionResult> GetPostsByAuthor([FromQuery] PostQueryParamsDTO postParams)
        {
            var query = new GetPostsByAuthorQuery(postParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var query = new GetPostByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostDTO postData)
        {
            var command = new CreatePostCommand(postData);
            var result = await _mediator.Send(command);
            return Created("Post created successfully", result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(string id, string title, string content, string authorId)
        {
            var post = await _postService.UpdatePost(id, title, content, authorId);

            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(string id)
        {
            var command = new DeletePostCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("translate/{id}")]
        public async Task<IActionResult> TranslatePost(string id)
        {
            var command = new GetTranslatedPostQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}