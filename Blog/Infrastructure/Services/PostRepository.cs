using Blog.Domain.Models;
using Blog.Infrastructure.Factories;
using Blog.Infrastructure.Filters;
using Blog.Web.Requests;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _postRepository;
        private readonly PostFilters _postFilters;

        public PostRepository(MongoDbFactory mongoDbFactory, PostFilters postFilters)
        {
            _postRepository = mongoDbFactory.GetPostCollection();
            _postFilters = postFilters;
        }

        public async Task<List<Post>> GetFilteredPostsAsync(PostQueryParametersRequest query, CancellationToken cancellationToken = default)
        {
            var filters = _postFilters.GetPostsFilter(query);

            var posts = await _postRepository.Find(filters).ToListAsync();

            return posts;
        }

        public async Task<List<Post>> GetFilteredPostsByAuthorIdAsync(PostQueryParametersRequest query, string userId, CancellationToken cancellationToken = default)
        {
            var filters = _postFilters.GetPostsFilter(query, userId);

            var posts = await _postRepository.Find(filters).ToListAsync();

            return posts;
        }

        public async Task<Post> GetPostByIdAsync(string postId, CancellationToken cancellationToken = default)
        {
            return await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Post post, CancellationToken cancellationToken = default)
        {
            await _postRepository.InsertOneAsync(post);
        }

        public async Task<bool> DeleteByIdAsync(string postId, CancellationToken cancellationToken = default)
        {
            var result = await _postRepository.DeleteOneAsync(x => x.Id == postId);

            return result.DeletedCount != 0;
        }

        public async Task<Post> UpdateAsync(Post post, CancellationToken cancellationToken = default)
        {
            await _postRepository.ReplaceOneAsync(x => x.Id == post.Id, post, cancellationToken: cancellationToken);

            return post;
        }

        public async Task<bool> ExistsAsync(string postId, CancellationToken cancellationToken = default)
        {
            return await _postRepository.Find(x => x.Id == postId).AnyAsync();
        }

        public async Task<bool> DoesPostBelongToAuthorAsync(string postId, string authorId, CancellationToken cancellationToken = default)
        {
            return await _postRepository.Find(x => x.Id == postId && x.AuthorId == authorId).AnyAsync();
        }
    }
}