using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface IFirebaseAuthAdapter
{
    Task<User?> AuthenticateUserAsync(string username, string password);
}