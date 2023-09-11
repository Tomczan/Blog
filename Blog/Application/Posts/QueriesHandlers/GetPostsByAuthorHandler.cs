using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetPostsByAuthorHandler : IRequestHandler<GetPostsByAuthorQuery, List<Post>>
    {
        private readonly IPostRepository _postService;
        private readonly IUserRepository _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetPostsByAuthorHandler(IPostRepository postService, IUserRepository userService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Post>> Handle(GetPostsByAuthorQuery request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _postService.GetFilteredPostsByAuthorIdAsync(request.PostQueryParams, userId);
        }
    }
}