using Blog.Domain.Models;

namespace Blog.Infrastructure.Services
{
    public interface IPostService
    {
        Task<Post> CreatePost(string title, string content, string authorId);

        void DeletePost(string postId);

        Task<Post> UpdatePost(string postId, string newTitle, string newContent, string authorId);

        Task<Post> GetPostById(string postId);

        Task<List<Post>> GetPostByTitle(string title);

        Task<List<Post>> GetAllPosts();
    }
}