using Stockly.Core.Common;

namespace Stockly.Core.Entities;

public record User : IEntity, IAggregateRoot
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    public string JwtToken { get; init; }
}