using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Domain.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? Nickname { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? PostsIds { get; set; }

        public User()
        { }

        protected User(string login, string password)
        {
            Id = ObjectId.GenerateNewId().ToString();
            Login = login;
            Password = HashPassword(password);
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public static User Create(string login, string password)
        {
            return new User(login, password);
        }

        public static string HashPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public bool VerifyPassword(string password)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(Password);
        }
    }
}