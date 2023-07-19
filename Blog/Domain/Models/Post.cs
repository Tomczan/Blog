using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Blog.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Post()
        { }

        protected Post(string title, string content)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public static Post Create(string title, string content)
        {
            return new Post(title, content);
        }

        public void Update(string newTitle, string newContent)
        {
            Title = newTitle;
            Content = newContent;
            UpdatedDate = DateTime.Now;
        }
    }
}