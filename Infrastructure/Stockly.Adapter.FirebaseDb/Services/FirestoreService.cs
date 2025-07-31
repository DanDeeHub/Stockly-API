using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public class FirestoreService
{
    public FirestoreDb Db { get; }
    public FirestoreService(IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var projectId = config["Firebase:ProjectId"]
                        ?? throw new ArgumentNullException(nameof(config),
                            "Firebase:ProjectId configuration is missing");

        var credentialsPath = config["Firebase:CredentialsPath"]
                              ?? throw new ArgumentNullException(nameof(config),
                                  "Firebase:CredentialsPath configuration is missing");

        Db = new FirestoreDbBuilder
        {
            ProjectId = projectId,
            CredentialsPath = credentialsPath
        }.Build();
    }
}