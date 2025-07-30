using Stockly.Core.Common;

namespace Stockly.Core.Entities;

public record User(Guid Id, string Username, string Password, string Email, string Role, string JwtToken)
    : IEntity, IAggregateRoot;