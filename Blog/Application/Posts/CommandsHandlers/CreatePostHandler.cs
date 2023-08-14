using Blog.Application.Posts.Commands;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public CreatePostHandler(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByLogin(request.UserLogin);
            return await _postService.CreatePost(request.Title, request.Content, user.Id);
        }
    }
}