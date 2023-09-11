using Blog.Application.Posts.Commands;
using Blog.Application.Posts.Queries;
using Blog.Web.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _contextAccessor;

        public PostController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            _mediator = mediator;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryParametersRequest postParams)
        {
            var query = new GetPostsQuery(postParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("my-posts/")]
        [Authorize]
        public async Task<IActionResult> GetPostsByAuthor([FromQuery] PostQueryParametersRequest postParams)
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
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var command = new CreatePostCommand(request);
            var result = await _mediator.Send(command);
            return Created("Post created successfully", result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePost(UpdatePostRequest request)
        {
            var authorId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new UpdatePost.Command(request.PostId, request.Title, request.Content, authorId);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var command = new DeletePostCommand(postId);
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