using Blog.Domain.Models;
using TranslatorApp;

namespace Blog.Application.Interfaces
{
    public interface ITranslatorService
    {
        Task<TextReply> TranslatePost(Post post);
    }
}