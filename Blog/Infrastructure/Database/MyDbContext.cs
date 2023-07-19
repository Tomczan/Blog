using Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, "BlogDB.db");
        }

        public virtual DbSet<Post> Posts { get; set; }
        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>();
        }
    }
}