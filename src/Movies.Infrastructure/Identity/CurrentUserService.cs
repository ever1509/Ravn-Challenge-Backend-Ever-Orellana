using Microsoft.AspNetCore.Http;
using Movies.Application.Common.Interfaces;
using System.Linq;

namespace Movies.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.Claims?.Single(u => u.Type == "id").Value;
        }
    }
}
