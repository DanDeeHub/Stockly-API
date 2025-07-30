using Stockly.Core.Entities;

namespace Stockly.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}