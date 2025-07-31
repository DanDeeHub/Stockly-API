using System.Drawing;
using Google.Cloud.Firestore;
using Stockly.Core.Entities;
using static Stockly.Infrastructure.Adapter.FirebaseDb.Constants.FirebaseConstants;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Store;

[FirestoreData]
public class ProductDocument
{
    [FirestoreDocumentId]
    public string Id { get; init; } = string.Empty;

    [FirestoreProperty(Properties.Name)]
    public string Name { get; init; } = string.Empty;

    [FirestoreProperty(Properties.Category)]
    public string Category { get; init; } = string.Empty;

    [FirestoreProperty(Properties.Stock)]
    public int Stock { get; init; }

    [FirestoreProperty(Properties.Price)]
    public double Price { get; init; }

    [FirestoreProperty(Properties.StatusColor)]
    public int StatusColorArgb { get; init; }

    [FirestoreProperty(Properties.CreatedAt)]
    public Timestamp CreatedAt { get; init; }

    public Product ToEntity() => new(
        Id: Id,
        Name: Name,
        Category: Category,
        Stock: Stock,
        Price: Price,
        StatusColor: StatusColorArgb == 0 ? Color.Empty : Color.FromArgb(StatusColorArgb),
        CreatedAt: CreatedAt.ToDateTime()
    );
}