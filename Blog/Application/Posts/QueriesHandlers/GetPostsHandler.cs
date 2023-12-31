﻿using Blog.Application.Posts.Queries;
using Blog.Domain.Models;
using Blog.Infrastructure.Services;
using MediatR;

namespace Blog.Application.Posts.QueriesHandlers
{
    public class GetPostsHandler : IRequestHandler<GetPostsQuery, List<Post>>
    {
        private readonly IPostRepository _postService;

        public GetPostsHandler(IPostRepository postService)
        {
            _postService = postService;
        }

        public async Task<List<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return await _postService.GetFilteredPostsAsync(request.PostQueryParams);
        }
    }
}