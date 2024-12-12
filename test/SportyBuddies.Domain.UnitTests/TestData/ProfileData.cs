using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.UnitTests.TestData;

public static class ProfileData
{
    public static readonly string Name = "John";
    public static readonly string Description = "Profile description";
    public static readonly Gender Gender = Gender.Male;
    public static readonly DateOnly DateOfBirth = new(2000, 1, 1);
}