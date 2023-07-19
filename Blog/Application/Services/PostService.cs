using Blog.Application.Interfaces;
using Blog.Domain.Interfaces;
using Blog.Domain.Models;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task CreatePost(string title, string content)
        {
            var post = new Post(title, content)
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _postRepository.Create(post);
        }

        public async Task DeletePost(Guid postId)
        {
            var post = _postRepository.GetById(postId);

            if (post != null)
            {
                _postRepository.Delete(post.Id);
            }
            else
            {
                throw new Exception("Post not found");
            }
        }

        public async Task UpdatePost(Guid postId, string newTitle, string newContent)
        {
            var post = _postRepository.GetById(postId);

            if (post != null)
            {
                post.Title = newTitle;
                post.Content = newContent;
                post.UpdatedDate = DateTime.Now;

                _postRepository.Update(post);
            }
            else
            {
                throw new Exception("Post not found");
            }
        }

        public async Task<Post> GetPost(Guid postId)
        {
            return await _postRepository.GetById(postId);
        }
    }
}