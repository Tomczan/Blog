﻿using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IMediator _mediator;

        public PostController(PostService postService, IMediator mediator)
        {
            _postService = postService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPostsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _postService.GetPostById(id);

            return Ok(post);
        }

        [HttpGet("search/{title}")]
        public async Task<List<Post>> GetPostsByTitle(string title)
        {
            var posts = await _postService.GetPostByTitle(title);

            return posts;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string title, string content, string authorId)
        {
            return Created("", await _postService.CreatePost(title, content, authorId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(string id, string title, string content, string authorId)
        {
            var post = await _postService.UpdatePost(id, title, content, authorId);

            return Ok(post);
        }

        [HttpDelete]
        public IActionResult DeletePost(string id)
        {
            _postService.DeletePost(id);

            return Ok(id);
        }
    }
}