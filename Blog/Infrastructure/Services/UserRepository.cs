using Blog.Domain.Models;
using Blog.Infrastructure.Factories;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userRepository;

        public UserRepository(MongoDbFactory mongoDbFactory)
        {
            _userRepository = mongoDbFactory.GetUserCollection();
        }

        public async Task<User> CreateAsync(string name, string password, CancellationToken cancellationToken = default)
        {
            var user = User.Create(name, password);

            await _userRepository.InsertOneAsync(user);

            return user;
        }

        public async Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _userRepository.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByLogin(string login, CancellationToken cancellationToken = default)
        {
            return await _userRepository.Find(x => x.Login == login).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _userRepository.Find(_ => true).ToListAsync();
        }

        public async Task<User> LoginAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var hashedPassword = User.HashPassword(password);
            var result = await _userRepository.Find(x => x.Login == login && x.Password == hashedPassword).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> DoesUserExistsAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _userRepository.Find(x => x.Id == userId).AnyAsync();
        }
    }
}