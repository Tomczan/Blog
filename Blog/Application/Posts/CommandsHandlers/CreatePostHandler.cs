using Blog.Application.Posts.Commands;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IPostRepository _postService;
        private readonly IUserRepository _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreatePostHandler(IPostRepository postService, IUserRepository userService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _userService.DoesUserExistsAsync(userId))
            {
                throw new Exception("User with that ID does not exists.");
            }

            var post = Post.Create(request.Title,
                request.Content,
                userId);

            await _postService.CreateAsync(post);

            return post;
        }
    }
}