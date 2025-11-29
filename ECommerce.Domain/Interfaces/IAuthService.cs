using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces;

public interface IAuthService
{
    Task<User?> Register(string fullName, string email, string password);
    Task<User?> Login(string email, string password);
    string GenerateToken(User user);
}
