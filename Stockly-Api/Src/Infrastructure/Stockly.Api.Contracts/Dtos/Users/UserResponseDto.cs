namespace Stockly.Infrastructure.Api.Contracts.Dtos.Users;

public class UserResponseDto(Guid id, string username, string password, string email, string role, string jwtToken)
{
    public Guid Id { get; init; } = id;
    public string Username { get; init; } = username;
    public string Password { get; init; } = password;
    public string Email { get; init; } = email;
    public string Role { get; init; } = role;
    public string JwtToken { get; init; } = jwtToken;
}