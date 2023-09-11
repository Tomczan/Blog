using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Requests
{
    public class CreatePostRequest
    {
        [Required]
        public string Title { get; set; }

        public string Content { get; set; }
    }
}