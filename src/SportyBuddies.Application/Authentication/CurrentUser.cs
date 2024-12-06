namespace SportyBuddies.Application.Authentication;

public record CurrentUser(Guid Id, string Email, IEnumerable<string> Roles, DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}