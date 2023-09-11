using Blog.Domain.Models;
using Blog.Web.Requests;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostsByAuthorQuery : IRequest<List<Post>>
    {
        public PostQueryParametersRequest PostQueryParams { get; }

        public GetPostsByAuthorQuery(PostQueryParametersRequest postQueryParams)
        {
            PostQueryParams = postQueryParams;
        }
    }
}