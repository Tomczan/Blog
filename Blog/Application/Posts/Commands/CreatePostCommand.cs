﻿using Blog.Application.Dtos;
using Blog.Domain.Models;
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

        [Required]
        public string AuthorId { get; set; }

        public CreatePostCommand(CreatePostDTO postData)
        {
            Title = postData.Title;
            Content = postData.Content;
            AuthorId = postData.AuthorId;
        }
    }
}