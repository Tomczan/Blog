using Blog.Application.Posts.Queries;
using Blog.Infrastructure.Services;
using MediatR;
using TranslatorApp;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetTranslatedPostHandler : IRequestHandler<GetTranslatedPostQuery, TextReply>
    {
        private readonly TranslatorService _translatorService;
        private readonly PostService _postService;

        public GetTranslatedPostHandler(PostService postService, TranslatorService translatorService)
        {
            _postService = postService;
            _translatorService = translatorService;
        }

        public async Task<TextReply?> Handle(GetTranslatedPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetPostById(request.PostId);
            var translatedPost = await _translatorService.TranslatePost(post);
            return translatedPost == null ? null : translatedPost;
        }
    }
}