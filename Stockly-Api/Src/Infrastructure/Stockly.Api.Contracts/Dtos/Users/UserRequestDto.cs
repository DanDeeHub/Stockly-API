using System.ComponentModel.DataAnnotations;

namespace Stockly.Infrastructure.Api.Contracts.Dtos.Users;

public class UserRequestDto
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}