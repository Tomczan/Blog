using DeepL;
using Microsoft.AspNetCore.Identity;
using TranslatorApp.Interfaces;

namespace TranslatorApp.Services
{
    public class DeepLService : IDeepLService
    {
        private readonly string _authKey;
        private readonly DeepL.Translator _translator;

        public DeepLService()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<Program>().Build();
            _authKey = configuration.GetSection("deepl")["authKey"];
            _translator = new DeepL.Translator(_authKey);
        }

        public async Task<TextReply> TranslatePost(string title, string content, string delimiter = "*")
        {
            var translation = await _translator.TranslateTextAsync(
                $"{title} {delimiter} {content}",
                LanguageCode.Polish,
                LanguageCode.EnglishAmerican);

            var translatedTextArr = translation.ToString().Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var translatedReply = new TextReply
            {
                TranslatedPostTitle = translatedTextArr[0].Trim(),
                TranslatedPostBody = translatedTextArr[1].Trim(),
            };

            return translatedReply;
        }
    }
}