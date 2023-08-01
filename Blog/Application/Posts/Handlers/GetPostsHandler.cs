using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.Handlers
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, List<Post>>
    {
        private readonly PostService _postService;

        public GetPostsHandler(PostService postService)
        {
            _postService = postService;
        }

        public async Task<List<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetPosts(request.PostQueryParams);
        }
    }
}