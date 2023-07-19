using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetById(Guid id);

        Task<Post> Create(Post post);

        void Update(Post post);

        void Delete(Guid id);
    }
}