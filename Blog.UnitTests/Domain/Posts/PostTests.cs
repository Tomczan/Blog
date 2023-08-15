using Blog.Domain.Models;
using DnsClient.Protocol;
using FluentAssertions;

namespace Blog.UnitTests.Domain.Posts
{
    public class PostTests
    {
        [Fact]
        public void CreatePost_ShouldReturnNonNullPost()
        {
            // Arrange + Act
            var post = CreatePost();

            // Assert
            post.Should().NotBeNull();
        }

        [Theory]
        [InlineData("First title")]
        [InlineData("Trackmania")]
        [InlineData("Example post title")]
        public void CreatePostWithTitle_ShouldReturnPostWithThatTitle(string title)
        {
            // Arrange + Act
            var post = CreatePost(title: title);

            // Assert
            post.Title.Should().Be(title);
        }

        [Fact]
        public void CreatePostCreateDate_ShouldBeEarlierThanCurrentTime()
        {
            // Arrange + Act
            var post = CreatePost();

            // Assert
            post.CreatedDate.Should().BeBefore(DateTime.Now);
        }

        private Post CreatePost(
            string title = "Title",
            string content = "content",
            string authorId = "43123eqwew")
        {
            return Post.Create(title, content, authorId);
        }
    }
}