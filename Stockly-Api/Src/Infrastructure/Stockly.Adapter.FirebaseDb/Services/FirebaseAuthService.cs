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
        var docId = snapshot.Documents[0].Id;
        
        if (userData["password"].ToString() != password)
            return null;

        return new User(
            Id: new Guid(docId),
            Username: username,
            Password: password,
            Email: userData["email"].ToString() ?? string.Empty,
            Role: userData["role"].ToString() ?? string.Empty,
            JwtToken: userData["jwtToken"].ToString() ?? string.Empty
        );
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var snapshot = await _db.Collection("users")
            .Document(userId)
            .GetSnapshotAsync();

        if (!snapshot.Exists) return null;

        var userData = snapshot.ConvertTo<Dictionary<string, object>>();
        
        return new User(
            Id: new Guid(userId),
            Username: userData["username"].ToString() ?? string.Empty,
            Password: userData["password"].ToString() ?? string.Empty,
            Email: userData["email"].ToString() ?? string.Empty,
            Role: userData["role"].ToString() ?? string.Empty,
            JwtToken: userData["jwtToken"].ToString() ?? string.Empty
        );
    }
}