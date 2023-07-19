using Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracje modelu danych
            // modelBuilder.Entity<Post>().ToTable("Posts");
            // ...

            base.OnModelCreating(modelBuilder);
        }
    }
}