using Blog.Application.Posts.Commands;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreatePostHandler(PostService postService, UserService userService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userLogin = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.GetUserByLogin(userLogin);
            return await _postService.CreatePost(request.Title, request.Content, user.Id);
        }
    }
}