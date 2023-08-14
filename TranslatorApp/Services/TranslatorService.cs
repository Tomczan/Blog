using Grpc.Core;

namespace TranslatorApp.Services
{
    public class TranslatorService : Translator.TranslatorBase
    {
        private readonly ILogger<TranslatorService> _logger;
        private readonly DeepLService _deepLService;

        public TranslatorService(ILogger<TranslatorService> logger, DeepLService deepLService)
        {
            _logger = logger;
            _deepLService = deepLService;
        }

        public override async Task<TextReply> TranslatePost(TextRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Received request for translation. Post Title: {request.PostTitle}, Post Body: {request.PostBody}");

            var translatedPost = await _deepLService.TranslatePost(request.PostTitle, request.PostBody);

            _logger.LogInformation($"Sending translated response. Translated Title: {translatedPost.TranslatedPostTitle}, Translated Body: {translatedPost.TranslatedPostBody}");

            return translatedPost;
        }
    }
}