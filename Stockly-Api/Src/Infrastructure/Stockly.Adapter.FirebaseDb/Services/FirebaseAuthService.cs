using Google.Cloud.Firestore;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public class FirebaseAuthService(FirestoreService firestoreService) : IAuthService
{
    private readonly FirestoreDb _db = firestoreService.Db;

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var snapshot = await _db.Collection("users")
            .WhereEqualTo("username", username)
            .GetSnapshotAsync();

        if (snapshot.Count == 0) return null;
        
        var userData = snapshot.Documents[0].ConvertTo<Dictionary<string, object>>();
        
        return userData["password"].ToString() == password
            ? new User 
            {
                Id = new Guid(snapshot.Documents[0].Id),
                Username = username,
                Email = userData["email"].ToString() ?? string.Empty,
                Role = userData["role"].ToString() ?? string.Empty
            }
            : null;
    }

    public Task<User?> GetUserByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }
}