using Blog.Application.Dtos;
using Blog.Domain.Models;
using Blog.Infrastructure.Factories;
using Blog.Infrastructure.Filters;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _postRepository;
        private readonly PostFilters _postFilters;

        public PostService(MongoDbFactory mongoDbFactory, PostFilters postFilters)
        {
            _postRepository = mongoDbFactory.GetPostCollection();
            _postFilters = postFilters;
        }

        public async Task<List<Post>> GetPosts(PostQueryParamsDTO query)
        {
            var filters = _postFilters.GetPostsFilter(query);

            var posts = await _postRepository.Find(filters).ToListAsync();

            return posts;
        }

        public async Task<Post> GetPostById(string postId)
        {
            return await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<Post> CreatePost(string title, string content, string authorId)
        {
            var post = Post.Create(title, content, authorId);

            await _postRepository.InsertOneAsync(post);

            return post;
        }

        public async Task<bool> DeletePost(string postId)
        {
            var post = await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();

            if (post != null)
            {
                var result = await _postRepository.DeleteOneAsync(x => x.Id == postId);

                if (result.DeletedCount == 0)
                {
                    throw new Exception("Failed to delete the post");
                }

                return result.IsAcknowledged;
            }
            else
            {
                throw new Exception("Post not found");
            }
        }

        public async Task<Post> UpdatePost(string postId, string newTitle, string newContent, string authorId)
        {
            var post = await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync() ?? throw new Exception($"Post with {postId} does not exist.");

            // temporary solution, fix cuz it will return table with new id on update!!
            var newPost = Post.Create(newTitle, newContent, authorId);

            //post.Update(newTitle, newContent);
            await _postRepository.ReplaceOneAsync(x => x.Id == postId, newPost);

            return post;
        }
    }
}