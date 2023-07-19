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

        public async Task<Post> CreatePost(string title, string content)
        {
            var post = new Post(title, content)
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _postRepository.Create(post);

            return post;
        }

        public async void DeletePost(Guid postId)
        {
            var post = await _postRepository.GetById(postId);

            if (post != null)
            {
                _postRepository.Delete(postId);
            }
            else
            {
                throw new Exception("Post not found");
            }
        }

        public async Task<Post> UpdatePost(Guid postId, string newTitle, string newContent)
        {
            var post = await _postRepository.GetById(postId);

            if (post != null)
            {
                post.Title = newTitle;
                post.Content = newContent;
                post.UpdatedDate = DateTime.Now;

                _postRepository.Update(post);
                return post;
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

        void IPostService.DeletePost(Guid postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll();
        }
    }
}