using Blog.Domain.Models;
using Blog.Infrastructure.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Blog.Infrastructure.Factories
{
    public class MongoDbFactory
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly MongoDbSettings _mongoDbSettings;

        public MongoDbFactory(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings.Value;
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Post> GetPostCollection()
        {
            return _mongoDatabase.GetCollection<Post>(_mongoDbSettings.PostsCollectionName);
        }

        public IMongoCollection<User> GetUserCollection()
        {
            var userCollection = _mongoDatabase.GetCollection<User>(_mongoDbSettings.UsersCollectionName);

            var uniqueOptions = new CreateIndexOptions { Unique = true };
            userCollection.Indexes.CreateOne("{ Login : 1 }", uniqueOptions);

            return userCollection;
        }
    }
}