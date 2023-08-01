using Blog.Application.Dtos;
using Blog.Domain.Models;
using Blog.Infrastructure.Factories;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _postRepository;

        public PostService(MongoDbFactory mongoDbFactory)
        {
            _postRepository = mongoDbFactory.GetPostCollection();
        }

        public async Task<List<Post>> GetPosts(PostQueryParamsDTO query)
        {
            // https://stackoverflow.com/questions/32227284/mongo-c-sharp-driver-building-filter-dynamically-with-nesting
            // https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/builders/
            var builder = Builders<Post>.Filter;

            var filters = builder.Empty;

            if (query.Title != null)
            {
                filters &= builder.Eq(x => x.Title, query.Title);
            }

            if (query.CreatedDate != null)
            {
                filters &= builder.Gt(x => x.CreatedDate, query.CreatedDate);
            }

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

        public async Task DeletePost(string postId)
        {
            var post = await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();

            if (post != null)
            {
                var result = await _postRepository.DeleteOneAsync(x => x.Id == postId);

                if (result.DeletedCount == 0)
                {
                    throw new Exception("Failed to delete the post");
                }
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