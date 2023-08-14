using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetPostsByAuthorHandler : IRequestHandler<GetPostsByAuthorQuery, List<Post>>
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetPostsByAuthorHandler(PostService postService, UserService userService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Post>> Handle(GetPostsByAuthorQuery request, CancellationToken cancellationToken)
        {
            var userLogin = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.GetUserByLogin(userLogin);
            return await _postService.GetPostsByAuthor(request.PostQueryParams, user);
        }
    }
}