namespace Stockly.Infrastructure.Api.Contracts.Dtos.Products;

public record ProductResponseDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Category { get; init; }
    public int Stock { get; init; }
    public string Status { get; init; }
    public string StatusColorHex { get; init; }
    public double Price { get; init; }
    public DateTime CreatedAt { get; init; }
}