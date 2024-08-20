using SportyBuddies.Domain.Entities;

namespace SportyBuddies.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}