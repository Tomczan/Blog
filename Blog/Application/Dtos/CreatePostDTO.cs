using Blog.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Dtos
{
    public class CreatePostDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }
    }
}