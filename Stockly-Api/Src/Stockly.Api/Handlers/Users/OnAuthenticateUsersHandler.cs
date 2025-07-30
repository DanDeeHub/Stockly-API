using System.ComponentModel.DataAnnotations;
using MediatR;
using Stockly.Api.Commands.Users;
using Stockly.Core.Entities;
using Stockly.Core.Interfaces;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Handlers.Users;

public class OnAuthenticateUsersHandler(IAuthService authService)  
    : IRequestHandler<OnAuthenticateUsersCommand, UserResponseDto>
{
    
    public async Task<UserResponseDto> Handle(OnAuthenticateUsersCommand request, CancellationToken cancellationToken)
    {
        var loginIdentifier = request.userName ?? request.email 
            ?? throw new ValidationException("Username or email must be provided");

        var user = await authService.AuthenticateAsync(
            loginIdentifier,
            request.password);
        
        if (user != null)
            return new UserResponseDto(
                id: user.Id,
                username: user.Username,
                password: user.Password,
                email: user.Email,
                role: user.Role,
                jwtToken: user.JwtToken);
        throw new Exception("Invalid username or password");
    }
}