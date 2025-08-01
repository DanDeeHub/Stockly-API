using System.Drawing;
using AutoMapper;
using Stockly.Core.Entities;
using Stockly.Infrastructure.Api.Contracts.Dtos.Products;

namespace Stockly.Api.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Request -> Entity (Record-compatible mapping)
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Status, opt
                => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusColorHex, opt
                => opt.MapFrom(src => ColorToHex(src.StatusColor)));

        // IEnumerable support (optimized version)
        CreateMap<IEnumerable<Product>, IEnumerable<ProductResponseDto>>()
            .ConvertUsing(src => src.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Stock = p.Stock,
                Status = p.Status,
                StatusColorHex = ColorToHex(p.StatusColor),
                Price = p.Price,
                CreatedAt = p.CreatedAt
            }));
    }

    private static string ColorToHex(Color color)
        => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}