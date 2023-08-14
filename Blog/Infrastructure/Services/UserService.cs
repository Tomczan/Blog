﻿using Blog.Domain.Models;
using Blog.Infrastructure.Factories;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userRepository;

        public UserService(MongoDbFactory mongoDbFactory)
        {
            _userRepository = mongoDbFactory.GetUserCollection();
        }

        public async Task<User> CreateUser(string name, string password)
        {
            var user = User.Create(name, password);

            await _userRepository.InsertOneAsync(user);

            return user;
        }

        public async Task<User> GetUser(string id)
        {
            return await _userRepository.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _userRepository.Find(x => x.Login == login).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.Find(_ => true).ToListAsync();
        }

        public async Task<User> Login(string login, string password)
        {
            var hashedPassword = User.HashPassword(password);
            var result = await _userRepository.Find(x => x.Login == login && x.Password == hashedPassword).FirstOrDefaultAsync();

            return result;
        }
    }
}