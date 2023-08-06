using Blog.Application.Posts.Commands;
using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly PostService _postService;

        public CreatePostHandler(PostService postService)
        {
            _postService = postService;
        }

        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return await _postService.CreatePost(request.Title, request.Content, request.AuthorId);
        }
    }
}