using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.TestData;

public static class UserData
{
    public static readonly string Name = "John";
    public static readonly string Description = "User description";
    public static readonly Gender Gender = Gender.Male;
    public static readonly DateOnly DateOfBirth = new(2000, 1, 1);
}