using Blog.Application.Dtos;
using Blog.Domain.Models;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsByAuthorQuery : IRequest<List<Post>>
    {
        public PostQueryParamsDTO PostQueryParams { get; }

        public GetPostsByAuthorQuery(PostQueryParamsDTO postQueryParams)
        {
            PostQueryParams = postQueryParams;
        }
    }
}