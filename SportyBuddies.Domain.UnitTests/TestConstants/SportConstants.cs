using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.TestConstants;

public static class SportConstants
{
    public const string Name = "Football";
    public const string Description = "Description";
    public static readonly Guid Id = Guid.NewGuid();
    public static readonly List<User> Users = new();
}