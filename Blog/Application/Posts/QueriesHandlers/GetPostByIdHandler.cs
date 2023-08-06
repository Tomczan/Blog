using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly PostService _postService;

        public GetPostByIdHandler(PostService postService)
        {
            _postService = postService;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetPostById(request.PostId);
            return post == null ? null : post;
        }
    }
}