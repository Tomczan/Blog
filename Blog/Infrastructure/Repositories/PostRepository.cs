using Blog.Domain.Interfaces;
using Blog.Domain.Models;
using Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyDbContext _dbContext;

        public PostRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAll()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public async Task<Post> GetById(Guid id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Post> Create(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Update(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
            return post;
        }

        public async void Delete(Guid id)
        {
            var post = await GetById(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
        }
    }
}