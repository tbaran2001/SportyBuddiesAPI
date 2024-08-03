using SportyBuddiesAPI.Entities;

namespace SportyBuddiesAPI.Services;

public interface ITokenService
{
    string CreateToken(User user);
}