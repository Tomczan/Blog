using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAll();

        Task<Post> GetById(Guid id);

        Task<Post> Create(Post post);

        Task<Post> Update(Post post);

        void Delete(Guid id);
    }
}