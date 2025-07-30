using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stockly.Api.Commands.Users;
using Stockly.Infrastructure.Api.Contracts.Dtos.Users;

namespace Stockly.Api.Controllers;

[ApiController]
[Route("v1/users")] 
[Produces("application/json")]
public class UsersController(ISender requestSender, IMapper mapper) : ControllerBase
{
    private readonly ISender _requestSender = requestSender ?? throw new ArgumentNullException(nameof(requestSender));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponseDto>> AuthenticateUserAsync([FromBody] UserRequestDto dto)
    {
        var command = new OnAuthenticateUsersCommand(
            dto.Username,
            dto.Password,
            dto.Email);
        
        var resultDto = _mapper.Map<UserResponseDto>(await _requestSender.Send(command));
        return Ok(resultDto);
    }
}