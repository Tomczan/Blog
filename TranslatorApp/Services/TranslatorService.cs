using Grpc.Core;
using DeepL;
using Microsoft.Extensions.Configuration;

namespace TranslatorApp.Services
{
    public class TranslatorService : Translator.TranslatorBase
    {
        private readonly ILogger<TranslatorService> _logger;

        public TranslatorService(ILogger<TranslatorService> logger)
        {
            _logger = logger;
        }

        public override async Task<TextReply> TranslatePost(TextRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Received request for translation. Post Title: {request.PostTitle}, Post Body: {request.PostBody}");

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<Program>().Build();
            string authKey = configuration.GetSection("deepl")["authKey"];

            var translator = new DeepL.Translator(authKey);

            var translation = await translator.TranslateTextAsync(
                $"{request.PostTitle} * {request.PostBody}",
                LanguageCode.Polish,
                LanguageCode.EnglishAmerican);

            var translatedTextArr = translation.ToString().Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(translation);

            var translatedReply = new TextReply
            {
                TranslatedPostTitle = translatedTextArr[0].Trim(),
                TranslatedPostBody = translatedTextArr[1].Trim(),
            };

            // Logowanie wiadomości wychodzącej
            _logger.LogInformation($"Sending translated response. Translated Title: {translatedReply.TranslatedPostTitle}, Translated Body: {translatedReply.TranslatedPostBody}");

            return translatedReply;
        }
    }
}