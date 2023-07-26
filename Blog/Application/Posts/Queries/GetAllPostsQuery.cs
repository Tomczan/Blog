using Blog.Domain.Models;
using MediatR;

namespace Blog.Application.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<List<Post>>
    {
    }
}