using Blog.Domain.Models;

namespace Blog.Application.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAll();

        Task<Post> GetById(string id);

        Task<Post> Create(Post post);

        Task<Post> Update(Post post);

        void Delete(string id);
    }
}