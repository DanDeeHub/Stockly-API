using MediatR;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Commands.Users;

public record OnAuthenticateUsersCommand(
    string? userName,
    string password,
    string? email,
    string? role) : IRequest<UserResponseDto>;