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

        public async Task<Post> CreatePost(string title, string content, string authorId)
        {
            var post = Post.Create(title, content, authorId);

            await _postRepository.InsertOneAsync(post);

            return post;
        }

        public async void DeletePost(string postId)
        {
            var post = await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();

            if (post != null)
            {
                await _postRepository.DeleteOneAsync(x => x.Id == postId);
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

        public async Task<Post> GetPostById(string postId)
        {
            return await _postRepository.Find(x => x.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostByTitle(string title)
        {
            return await _postRepository.Find(x => x.Title == title).ToListAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postRepository.Find(_ => true).ToListAsync();
        }
    }
}