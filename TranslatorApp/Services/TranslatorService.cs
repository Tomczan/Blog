using Grpc.Core;

namespace TranslatorApp.Services
{
    public class TranslatorService : Translator.TranslatorBase
    {
        private readonly ILogger<TranslatorService> _logger;

        public TranslatorService(ILogger<TranslatorService> logger)
        {
            _logger = logger;
        }

        public override Task<TextReply> TranslatePost(TextRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Received request for translation. Post Title: {request.PostTitle}, Post Body: {request.PostBody}");

            var translatedReply = new TextReply
            {
                TranslatedPostTitle = "Translated title will be here",
                TranslatedPostBody = "Translated body will be here"
            };

            // Logowanie wiadomości wychodzącej
            _logger.LogInformation($"Sending translated response. Translated Title: {translatedReply.TranslatedPostTitle}, Translated Body: {translatedReply.TranslatedPostBody}");

            return Task.FromResult(translatedReply);
        }
    }
}