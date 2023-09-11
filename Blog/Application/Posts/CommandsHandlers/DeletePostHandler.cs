using Blog.Application.Posts.Commands;
using Blog.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Blog.Application.Posts.CommandsHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IPostRepository _postService;
        private readonly IHttpContextAccessor _contextAccessor;

        public DeletePostHandler(IPostRepository postService, IHttpContextAccessor contextAccessor)
        {
            _postService = postService;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            if (!await _postService.ExistsAsync(request.PostId, cancellationToken))
            {
                throw new Exception("Post id does not exists!");
            }

            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _postService.DoesPostBelongToAuthorAsync(request.PostId, userId, cancellationToken))
            {
                throw new Exception("Forbidden action. You cannot delete a post that does not belong to you.");
            }

            return await _postService.DeleteByIdAsync(request.PostId, cancellationToken);
        }
    }
}