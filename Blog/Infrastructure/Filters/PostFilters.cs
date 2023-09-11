using Blog.Domain.Models;
using Blog.Web.Requests;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Infrastructure.Filters
{
    public class PostFilters
    {
        //https://stackoverflow.com/questions/32227284/mongo-c-sharp-driver-building-filter-dynamically-with-nesting
        //https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/builders/
        public FilterDefinition<Post> GetPostsFilter(PostQueryParametersRequest query, string userId = "")
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

            if (!String.IsNullOrEmpty(userId))
            {
                filters &= builder.Where(post => post.AuthorId == userId);
            }

            return filters;
        }
    }
}