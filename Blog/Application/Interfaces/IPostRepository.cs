using Blog.Domain.Models;
using Blog.Web.Requests;

namespace Blog.Infrastructure.Services
{
    public interface IPostRepository
    {
        Task CreateAsync(Post post, CancellationToken cancellationToken = default);

        Task<bool> DeleteByIdAsync(string postId, CancellationToken cancellationToken = default);

        Task<Post> UpdateAsync(Post post, CancellationToken cancellationToken = default);

        Task<Post> GetPostByIdAsync(string postId, CancellationToken cancellationToken = default);

        Task<List<Post>> GetFilteredPostsAsync(PostQueryParametersRequest postQueryParams, CancellationToken cancellationToken = default);

        Task<List<Post>> GetFilteredPostsByAuthorIdAsync(PostQueryParametersRequest query, string userId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(string postId, CancellationToken cancellationToken = default);

        Task<bool> DoesPostBelongToAuthorAsync(string postId, string authorId, CancellationToken cancellationToken);
    }
}