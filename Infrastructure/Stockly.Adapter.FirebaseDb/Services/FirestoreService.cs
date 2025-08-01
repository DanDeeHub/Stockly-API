using System.Text;
using System.Text.Json;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public class FirestoreService
{
    public FirestoreDb Db { get; }
    public FirestoreService(IConfiguration config)
    {
        var projectId = config["Firebase:ProjectId"] ?? throw new ArgumentNullException(nameof(config));

        // Priority 1: Direct JSON (for production)
        if (config["Firebase:CredentialsJson"] is { } json)
        {
            Db = new FirestoreDbBuilder { ProjectId = projectId, JsonCredentials = json }.Build();
            return;
        }

        // Priority 2: Base64-encoded JSON (for Docker/CI)
        if (config["Firebase:CredentialsBase64"] is { } base64)
        {
            var jsonFromBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            Db = new FirestoreDbBuilder { ProjectId = projectId, JsonCredentials = jsonFromBase64 }.Build();
            return;
        }

        // Priority 3: File path (for development)
        var credentialsPath = config["Firebase:CredentialsPath"] ?? throw new ArgumentNullException(nameof(config));

        Db = new FirestoreDbBuilder { ProjectId = projectId, CredentialsPath = credentialsPath }.Build();
    }
}