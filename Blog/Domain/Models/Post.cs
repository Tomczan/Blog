using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        public Post()
        { }

        protected Post(string title, string content)
        {
            Id = ObjectId.GenerateNewId().ToString();
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