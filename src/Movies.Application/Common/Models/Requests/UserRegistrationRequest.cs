using Movies.Domain.Enums;

namespace Movies.Application.Common.Models.Requests
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; }
    }
}
