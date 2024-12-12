using FluentAssertions;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.UnitTests.TestData;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class UpdateProfileTests
{
    [Fact]
    public void Update_Should_SetPropertyValue()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        // Act
        user.Update(ProfileData.Name, ProfileData.Description, ProfileData.DateOfBirth, ProfileData.Gender);

        // Assert
        user.Name.Should().Be(ProfileData.Name);
        user.Description.Should().Be(ProfileData.Description);
        user.DateOfBirth.Should().Be(ProfileData.DateOfBirth);
        user.Gender.Should().Be(ProfileData.Gender);
    }
}