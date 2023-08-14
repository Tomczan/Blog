using Blog.Application.Dtos;
using Blog.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Infrastructure.Filters
{
    public class PostFilters
    {
        //https://stackoverflow.com/questions/32227284/mongo-c-sharp-driver-building-filter-dynamically-with-nesting
        //https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/builders/
        public FilterDefinition<Post> GetPostsFilter(PostQueryParamsDTO query, User? user = null)
        {
            var builder = Builders<Post>.Filter;
            var filters = builder.Empty;

            if (query.Title != null)
            {
                //filters &= builder.Eq(post => post.Title, query.Title);
                filters &= builder.Regex(x => x.Title, new BsonRegularExpression($".*{query.Title}.*", "i")); ;
            }

            if (query.CreatedDate != null)
            {
                filters &= builder.Gt(post => post.CreatedDate, query.CreatedDate);
            }

            if (user != null)
            {
                filters &= builder.Where(post => post.AuthorId == user.Id);
            }

            return filters;
        }
    }
}