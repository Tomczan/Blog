using Blog.Domain.Models;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetPostByIdQuery : IRequest<Post>
    {
        public string PostId { get; }

        public GetPostByIdQuery(string id)
        {
            PostId = id;
        }
    }
}