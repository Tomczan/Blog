using Blog.Domain.Models;
using Blog.Web.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Posts.Commands
{
    public class CreatePostCommand : IRequest<Post>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public CreatePostCommand(CreatePostRequest request)
        {
            Title = request.Title;
            Content = request.Content;
        }
    }
}