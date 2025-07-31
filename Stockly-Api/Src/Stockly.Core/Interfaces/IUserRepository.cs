using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task AddAsync(User user);
}