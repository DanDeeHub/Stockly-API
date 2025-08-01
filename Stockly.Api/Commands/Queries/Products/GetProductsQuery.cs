using MediatR;
using Stockly.Core.Entities;

namespace Stockly.Api.Commands.Queries.Products;

public record GetProductsQuery(int Limit = 25) : IRequest<IEnumerable<Product>>;