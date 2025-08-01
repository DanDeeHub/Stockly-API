namespace Stockly.Infrastructure.Api.Contracts.Dtos.Products;

public record ProductRequestDto(
    string Name,
    string Category,
    int Stock,
    decimal Price);