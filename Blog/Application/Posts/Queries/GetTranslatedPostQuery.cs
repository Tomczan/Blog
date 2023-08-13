using MediatR;
using TranslatorApp;

namespace Blog.Application.Posts.Queries
{
    public class GetTranslatedPostQuery : IRequest<TextReply>
    {
        public string PostId { get; }

        public GetTranslatedPostQuery(string id)
        {
            PostId = id;
        }
    }
}