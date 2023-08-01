using Blog.Application.Dtos;
using Blog.Domain.Models;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsQuery : IRequest<List<Post>>
    {
        public PostQueryParamsDTO PostQueryParams { get; }

        public GetPostsQuery(PostQueryParamsDTO postQueryParams)
        {
            PostQueryParams = postQueryParams;
        }
    }
}