using Blog.Application.Interfaces;
using Blog.Application.Posts.Queries;
using Blog.Infrastructure.Services;
using MediatR;
using TranslatorApp;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetTranslatedPostHandler : IRequestHandler<GetTranslatedPostQuery, TextReply>
    {
        private readonly ITranslatorService _translatorService;
        private readonly IPostRepository _postService;

        public GetTranslatedPostHandler(IPostRepository postService, ITranslatorService translatorService)
        {
            _postService = postService;
            _translatorService = translatorService;
        }

        public async Task<TextReply?> Handle(GetTranslatedPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetPostByIdAsync(request.PostId);
            var translatedPost = await _translatorService.TranslatePost(post);
            return translatedPost == null ? null : translatedPost;
        }
    }
}