namespace Stockly.Core.Models;

public record JwtConfig(
    string Key,
    string Issuer,
    string Audience,
    int ExpiryMinutes);