using Blog.Domain.Interfaces;
using Blog.Domain.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Blog.Infrastructure.Database;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MyDbContext _dbContext;

        public PostRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post GetById(Guid id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }

        public void Update(Post post)
        {
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var post = GetById(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();
            }
        }
    }
}