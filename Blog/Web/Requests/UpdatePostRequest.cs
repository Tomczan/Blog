using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Requests
{
    public class UpdatePostRequest
    {
        public string PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}