using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Api.Commands.Users;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("v1/users")] 
public class UsersController(ISender requestSender) : ControllerBase
{
    private readonly ISender _requestSender = requestSender ?? throw new ArgumentNullException(nameof(requestSender));
    
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponseDto>> AuthenticateUserAsync([FromBody] UserRequestDto dto)
    {
        var command = new OnAuthenticateUsersCommand(
            dto.Username,
            dto.Password,
            dto.Email,
            dto.Role);
        
        var result = await _requestSender.Send(command);
        return Ok(result);
    }
}