using Google.Cloud.Firestore;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;
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