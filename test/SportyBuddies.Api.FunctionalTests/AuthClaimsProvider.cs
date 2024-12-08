using System.Security.Claims;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.FunctionalTests;

public class AuthClaimsProvider
{
    public IList<Claim> Claims { get; }

    public AuthClaimsProvider(IList<Claim> claims)
    {
        Claims = claims;
    }

    public AuthClaimsProvider()
    {
        Claims = [
            new Claim(ClaimTypes.NameIdentifier, UserData.TestUserId.ToString()),
            new Claim(ClaimTypes.Email, "test@test.com"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("DateOfBirth", "01/01/2000")
        ];
    }
}