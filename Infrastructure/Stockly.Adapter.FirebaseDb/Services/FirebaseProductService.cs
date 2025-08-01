using Google.Cloud.Firestore;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Adapter.FirebaseDb.Constants;
using Stockly.Infrastructure.Adapter.FirebaseDb.Store;

namespace Stockly.Infrastructure.Adapter.FirebaseDb.Services;

public class FirebaseProductService(FirestoreService firestore) : IProductService
{
    private readonly CollectionReference _collection = firestore.Db.Collection("products");

    public async Task<Product> GetProductByIdAsync(string productId)
    {
        var snapshot = await _collection
            .Document(productId)
            .GetSnapshotAsync();

        var document = snapshot.ConvertTo<ProductDocument>();
        return document.ToEntity();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int limit = 25)
    {
        var snapshot = await _collection
            .Limit(limit)
            .GetSnapshotAsync();

        return snapshot.Documents
            .Select(doc => doc.ConvertTo<ProductDocument>())
            .Select(doc => doc.ToEntity());
    }

    public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string? name = null, string? category = null, decimal? minPrice = null,
        decimal? maxPrice = null, int limit = 25)
    {
        var query = _collection.Limit(limit);

        if (!string.IsNullOrEmpty(name))
            query = query.WhereEqualTo(FirebaseConstants.Properties.Name, name);
        if (!string.IsNullOrEmpty(category))
            query = query.WhereEqualTo(FirebaseConstants.Properties.Category, category);
        if (minPrice.HasValue)
            query = query.WhereGreaterThanOrEqualTo(FirebaseConstants.Properties.Price, (double)minPrice.Value);
        if (maxPrice.HasValue)
            query = query.WhereLessThanOrEqualTo(FirebaseConstants.Properties.Price, (double)maxPrice.Value);

        var snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents
            .Select(doc => doc.ConvertTo<ProductDocument>())
            .Select(doc => doc.ToEntity());
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var docRef = _collection.Document(product.Id);

        var productDocument = new ProductDocument
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Stock = product.Stock,
            Price = product.Price,
            StatusColorArgb = product.StatusColor.ToArgb(),
            CreatedAt = Timestamp.FromDateTime(product.CreatedAt)
        };

        await docRef.SetAsync(productDocument);
        return product;
    }
}