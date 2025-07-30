using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public abstract class FirestoreService(IConfiguration config)
{
    public FirestoreDb Db { get; } = new FirestoreDbBuilder
    {
        ProjectId = config["Firebase:ProjectId"],
        CredentialsPath = config["Firebase:CredentialsPath"]
    }.Build();
}