using System.Drawing;

namespace Stockly.Core.Entities;

public record Product(
    string Id,
    string Name,
    string Category,
    int Stock,
    double Price,
    Color StatusColor,
    DateTime CreatedAt)
{
    // Optional: Add computed properties
    public string Status => Stock > 0 ? "In Stock" : "Out of Stock";
};