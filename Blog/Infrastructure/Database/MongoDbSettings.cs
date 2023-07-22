namespace Blog.Infrastructure.Database
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PostsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}