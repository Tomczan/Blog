using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Dto
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}