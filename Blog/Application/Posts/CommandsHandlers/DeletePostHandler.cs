using Blog.Application.Posts.Commands;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly PostService _postService;

        public DeletePostHandler(PostService postService)
        {
            _postService = postService;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            return await _postService.DeletePost(request.PostId);
        }
    }
}