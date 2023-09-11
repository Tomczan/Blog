using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}