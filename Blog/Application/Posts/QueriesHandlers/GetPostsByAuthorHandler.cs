using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetPostsByAuthorHandler : IRequestHandler<GetPostsByAuthorQuery, List<Post>>
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public GetPostsByAuthorHandler(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public async Task<List<Post>> Handle(GetPostsByAuthorQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByLogin(request.AuthorLogin);
            return await _postService.GetPostsByAuthor(request.PostQueryParams, user);
        }
    }
}