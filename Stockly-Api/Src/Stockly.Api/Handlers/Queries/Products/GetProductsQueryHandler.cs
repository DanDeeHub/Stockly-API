using MediatR;
using Stockly.Api.Commands.Queries.Products;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;

namespace Stockly.Api.Handlers.Queries.Products;

public class GetProductsQueryHandler(IProductService productService) 
    : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await productService.GetProductsAsync(request.Limit);
    }
}