using Blog.Application.Dtos;
using Blog.Domain.Models;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsByAuthorQuery : IRequest<List<Post>>
    {
        public PostQueryParamsDTO PostQueryParams { get; }

        public string AuthorLogin { get; set; }

        public GetPostsByAuthorQuery(PostQueryParamsDTO postQueryParams, string authorLogin)
        {
            PostQueryParams = postQueryParams;
            AuthorLogin = authorLogin;
        }
    }
}