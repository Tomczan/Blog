using Blog.Domain.Models;

namespace Blog.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(string login, string password);

        Task<User> GetUser(string id);

        Task<List<User>> GetAllUsers();

        Task<bool> Login(string login, string password);
    }
}