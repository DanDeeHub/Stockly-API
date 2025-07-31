using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task<User?> GetUserByIdAsync(string userId);
}