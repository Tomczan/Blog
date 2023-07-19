using Blog.Domain.Models;

namespace Blog.Application.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePost(string title, string content);

        Task<Post> UpdatePost(Guid postId, string newTitle, string newContent);

        Task<Post> DeletePost(Guid postId);

        Task<Post> GetPost(Guid postId);
    }
}