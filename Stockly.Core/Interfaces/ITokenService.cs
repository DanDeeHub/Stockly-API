using System.Security.Claims;
using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<bool> ValidateTokenAsync(string token);
    Task<ClaimsPrincipal> GetPrincipalFromTokenAsync(string token);
}