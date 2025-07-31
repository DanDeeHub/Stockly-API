using Stockly.Core.Entities;
using Stockly.Core.Interfaces;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Repositories;

public class FirestoreUserRepository : IUserRepository
{
    public Task<User?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User user)
    {
        throw new NotImplementedException();
    }
}