using Movies.Application.Common.Models;
using Movies.Domain.Enums;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, UserRole? role);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<string> GetUserNameAsync(string userId);
    }
}
