using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(string productId);
    Task<IEnumerable<Product>> GetProductsAsync(int limit = 25);
    Task<IEnumerable<Product>> GetFilteredProductsAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null, int limit = 25);
    Task<Product> CreateProductAsync(Product product);
}