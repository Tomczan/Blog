using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Dtos
{
    public class PostQueryParamsDTO
    {
        public string? Title { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}