using System.ComponentModel.DataAnnotations;

namespace Stockly.Infrastructure.Api.Contracts.Dtos.Users;

public class UserRequestDto(string username, string password, string email, string role)
{
    [Required] public string Username { get; set; } = username;
    [Required] public string Password { get; set; } = password;
    public string Email { get; set; } = email;
    public string Role { get; set; } = role;
}