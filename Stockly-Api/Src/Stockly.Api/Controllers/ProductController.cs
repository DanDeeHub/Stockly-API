using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Api.Commands.Queries.Products;
using Stockly.Infrastructure.Api.Contracts.Dtos.Products;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("v1/products")]
[Produces("application/json")]
public class ProductController(ISender requestSender, IMapper mapper) : ControllerBase
{
    private readonly ISender _requestSender = requestSender ?? throw new ArgumentNullException(nameof(requestSender));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), 200)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int limit = 25)
    {
        var products = await _requestSender.Send(new GetProductsQuery(limit));
        var dtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return Ok(dtos);
    }
}