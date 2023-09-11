using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.Commands
{
    public static class UpdatePost
    {
        public sealed record Command(string PostId, string Title, string Content, string AuthorId) : IRequest<Unit>;

        public sealed class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IPostRepository _postService;
            private readonly IUserRepository _userRepository;

            public Handler(IPostRepository postService, IUserRepository userRepository)
            {
                _postService = postService;
                _userRepository = userRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await _userRepository.DoesUserExistsAsync(request.AuthorId, cancellationToken))
                {
                    throw new Exception("User with that id does not exists!");
                }

                if (!await _postService.DoesPostBelongToAuthorAsync(request.PostId, request.AuthorId, cancellationToken))
                {
                    throw new Exception("Forbidden action. You cannot update a post that does not belong to you.");
                }

                var post = await _postService.GetPostByIdAsync(request.PostId, cancellationToken);

                if (post is null)
                {
                    throw new Exception("Post does not exist.");
                }

                post.Title = request.Title;
                post.Content = request.Content;

                await _postService.UpdateAsync(post, cancellationToken);

                return Unit.Value;
            }
        }
    }
}