using Blog.Application.Interfaces;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(string name, string password)
        {
            var user = User.Create(name, password);

            await _userRepository.Create(user);

            return user;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<bool> Login(string name, string password)
        {
            return await _userRepository.Login(name, password);
        }
    }
}