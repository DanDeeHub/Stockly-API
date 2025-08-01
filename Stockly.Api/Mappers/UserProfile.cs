using AutoMapper;
using Stockly.Core.Entities;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}