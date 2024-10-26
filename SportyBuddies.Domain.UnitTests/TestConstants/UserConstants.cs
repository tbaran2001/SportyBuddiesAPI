using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.TestConstants;

public static class UserConstants
{
    public const string Name = "John";
    public const string Description = "Description";
    public static readonly Guid Id = Guid.NewGuid();
    public static readonly DateTime LastActive = DateTime.Now;
    public static readonly List<Sport> Sports = new();
    public static readonly UserPhoto MainPhoto = new();
    public static readonly List<UserPhoto> Photos = new();
    public static readonly Gender Gender = Gender.Male;
    public static readonly DateTime DateOfBirth = new(2000, 1, 1);
}