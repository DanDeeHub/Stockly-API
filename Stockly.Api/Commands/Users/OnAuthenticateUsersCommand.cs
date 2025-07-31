using MediatR;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Commands.Users;

public record OnAuthenticateUsersCommand(
    string? UserName,
    string Password,
    string? Email) : IRequest<UserResponseDto>;