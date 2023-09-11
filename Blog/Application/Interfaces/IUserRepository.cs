using Blog.Domain.Models;

namespace Blog.Infrastructure.Services
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(string login, string password, CancellationToken cancellationToken = default);

        Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);

        Task<User> LoginAsync(string login, string password, CancellationToken cancellationToken = default);

        Task<bool> DoesUserExistsAsync(string userId, CancellationToken cancellationToken = default);
    }
}