using Blog.Domain.Models;
using System.Threading.Tasks;

namespace Blog.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(string name, string password);

        Task<User> GetUser(Guid id);

        Task<bool> Login(string name, string password);

        Task<List<User>> GetAllUsers();
    }
}