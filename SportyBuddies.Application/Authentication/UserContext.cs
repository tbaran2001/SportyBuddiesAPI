using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Application.Authentication;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new InvalidOperationException("User Context is not available");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            throw new UnauthorizedException("User is not authenticated");

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(ClaimTypes.Email)!.Value;
        var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
        var dateOfBirthString = user.FindFirst("DateOfBirth")?.Value;

        var dateOfBirth = dateOfBirthString == null
            ? (DateOnly?)null
            : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

        return new CurrentUser(Guid.Parse(userId), email, roles, dateOfBirth);
    }
}