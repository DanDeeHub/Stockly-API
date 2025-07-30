using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Stockly.Core.Entities;
using Stockly.Infrastructure.Adapter.FirebaseDb.Interface;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public class FirebaseService : IFirebaseService
{
    private readonly FirestoreDb _db;
    
    public FirebaseService(IConfiguration configuration)
    {
        var firebaseConfig = configuration.GetSection("Firebase");
        var credentialsPath = firebaseConfig["CredentialsPath"];
        var projectId = firebaseConfig["ProjectId"];

        _db = new FirestoreDbBuilder
        {
            ProjectId = projectId,
            CredentialsPath = credentialsPath
        }.Build();
    }

    public FirestoreDb GetDatabase() => _db;
    public async Task<T?> GetDocumentAsync<T>(string collection, string id, CancellationToken ct = default) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task<string> AddDocumentAsync<T>(string collection, T entity, CancellationToken ct = default) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task UpdateDocumentAsync<T>(string collection, string id, T entity, CancellationToken ct = default) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task DeleteDocumentAsync(string collection, string id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> AuthenticateUserAsync(string usernameOrEmail, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByIdAsync(string userId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task RunTransactionAsync(Func<Transaction, Task> transactionHandler, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Query<T>(string collection) where T : class
    {
        throw new NotImplementedException();
    }
}