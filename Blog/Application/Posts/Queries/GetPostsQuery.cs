using Blog.Domain.Models;
using Blog.Web.Requests;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsQuery : IRequest<List<Post>>
    {
        public PostQueryParametersRequest PostQueryParams { get; }

        public GetPostsQuery(PostQueryParametersRequest postQueryParams)
        {
            PostQueryParams = postQueryParams;
        }
    }
}