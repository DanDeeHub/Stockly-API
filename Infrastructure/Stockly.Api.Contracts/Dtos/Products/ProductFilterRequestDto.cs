namespace Stockly.Infrastructure.Api.Contracts.Dtos.Products;

public record ProductFilterRequestDto(
    string? Name = null,
    string? Category = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    int? MinStock = null,
    int? MaxStock = null,
    int Limit = 25);