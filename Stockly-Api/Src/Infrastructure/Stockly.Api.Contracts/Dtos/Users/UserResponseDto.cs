namespace Stockly.Infrastructure.Api.Contracts.Dtos.Users;

public class UserResponseDto(Guid id, string username, string password, string email, string role, string jwtToken)
{
    public Guid Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string Password { get; set; }  = password;
    public string Email { get; set; } = email;
    public string Role { get; set; } = role;
    public string JwtToken { get; set; } = jwtToken;
}