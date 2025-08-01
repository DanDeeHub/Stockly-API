using MediatR;
using Stockly.Core.Entities;

namespace Stockly.Api.Commands.Queries.Products;

public record GetFilteredProductsQuery(
    string? Name = null,
    string? Category = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    int? MinStock = null,
    int? MaxStock = null,
    int Limit = 25) : IRequest<IEnumerable<Product>>;