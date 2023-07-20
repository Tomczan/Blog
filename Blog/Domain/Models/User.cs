using System;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public User()
        { }

        protected User(string name, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = HashPassword(password);
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public static User Create(string name, string password)
        {
            return new User(name, password);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password)
        {
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(Password);
        }
    }
}