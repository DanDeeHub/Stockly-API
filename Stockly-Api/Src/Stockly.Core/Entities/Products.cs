using System.Drawing;

namespace Stockly.Core.Entities;

public class Products
{
    public string Id { get; set; } 
    public string Name { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public string Status { get; set; }
    public Color StatusColor { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}