using MediatR;

namespace Blog.Application.Posts.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public string PostId { get; }

        public DeletePostCommand(string postId)
        {
            PostId = postId;
        }
    }
}