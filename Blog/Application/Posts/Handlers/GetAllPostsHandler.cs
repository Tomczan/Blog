using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.Handlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, List<Post>>
    {
        private readonly PostService _postService;

        public GetAllPostsHandler(PostService postService)
        {
            _postService = postService;
        }

        public async Task<List<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetAllPosts();
        }
    }
}