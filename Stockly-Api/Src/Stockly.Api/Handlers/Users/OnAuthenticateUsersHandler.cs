using System.ComponentModel.DataAnnotations;
using MediatR;
using Stockly.Api.Commands.Users;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Handlers.Users;

public class OnAuthenticateUsersHandler(IAuthService authService)
    : IRequestHandler<OnAuthenticateUsersCommand, UserResponseDto>
{

    public async Task<UserResponseDto> Handle(OnAuthenticateUsersCommand request, CancellationToken cancellationToken)
    {
        var loginIdentifier = request.UserName ?? request.Email
            ?? throw new ValidationException("Username or email must be provided");

        var user = await authService.AuthenticateAsync(
            loginIdentifier,
            request.Password);

        if (user != null)
            return new UserResponseDto(
                jwtToken: user.JwtToken);
        return null!;
    }
}