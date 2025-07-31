using Google.Cloud.Firestore;
using Stockly.Core.Entities;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Interface;

public interface IFirebaseService
{
    // Database Operations
    Task<T?> GetDocumentAsync<T>(string collection, string id, CancellationToken ct = default)
        where T : class;
    Task<string> AddDocumentAsync<T>(string collection, T entity, CancellationToken ct = default)
        where T : class;
    Task UpdateDocumentAsync<T>(string collection, string id, T entity, CancellationToken ct = default)
        where T : class;
    Task DeleteDocumentAsync(string collection, string id, CancellationToken ct = default);

    // Authentication
    Task<User?> AuthenticateUserAsync(string usernameOrEmail, CancellationToken ct = default);
    Task<User?> GetUserByIdAsync(string userId, CancellationToken ct = default);

    // Batch Operations
    Task RunTransactionAsync(Func<Transaction, Task> transactionHandler,
        CancellationToken ct = default);

    // Query Support
    IQueryable<T> Query<T>(string collection) where T : class;
}