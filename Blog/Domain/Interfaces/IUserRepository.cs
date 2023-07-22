using Blog.Domain.Models;

namespace Blog.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();

        Task<User> GetById(Guid id);

        Task<User> Create(User user);

        //Task<bool> Login(string name, string password);
    }
}