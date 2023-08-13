using Blog.Application.Interfaces;
using Blog.Domain.Models;
using TranslatorApp;

namespace Blog.Infrastructure.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly Translator.TranslatorClient _translatorClient;

        public TranslatorService(Translator.TranslatorClient translatorClient)
        {
            _translatorClient = translatorClient;
        }

        public async Task<TextReply> TranslatePost(Post post)
        {
            var translatedPost = await _translatorClient.TranslatePostAsync(new TextRequest
            {
                PostTitle = post.Title,
                PostBody = post.Content
            });

            return await Task.FromResult(translatedPost);
        }
    }
}