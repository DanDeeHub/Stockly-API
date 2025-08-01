using MediatR;
using Stockly.Api.Commands.Queries.Products;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;

namespace Stockly.Api.Handlers.Queries.Products;

public class GetFilteredProductsQueryHandler(IProductService productService,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetFilteredProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(
        GetFilteredProductsQuery request,
        CancellationToken cancellationToken)
    {
        var token = httpContextAccessor.HttpContext?
            .Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Missing token");

        if (!await tokenService.ValidateTokenAsync(token))
            throw new UnauthorizedAccessException("Invalid token");

        return await productService.GetFilteredProductsAsync(
            request.Name,
            request.Category,
            request.MinPrice,
            request.MaxPrice,
            request.Limit
        );
    }
}