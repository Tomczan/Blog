using Blog.Domain.Models;
using Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly MyDbContext _dbContext;

        public UserRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            return user;
        }

        //public async Task<bool> Login(string name, string password)
        //{
        //    var user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Name == name);
        //    return user.VerifyPassword(password);
        //}
    }
}