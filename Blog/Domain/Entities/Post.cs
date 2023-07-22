using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string? AuthorId { get; set; }

        public Post()
        { }

        protected Post(string title, string content, string authorId)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Title = title;
            Content = content;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            AuthorId = authorId;
        }

        public static Post Create(string title, string content, string authorId)
        {
            return new Post(title, content, authorId);
        }

        public void Update(string newTitle, string newContent)
        {
            Title = newTitle;
            Content = newContent;
            UpdatedDate = DateTime.Now;
        }
    }
}