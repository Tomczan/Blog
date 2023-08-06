using Blog.Domain.Models;

namespace Blog.Application.Interfaces
{
    public interface IAuthService
    {
        string CreateJwtToken(User user);
    }
}