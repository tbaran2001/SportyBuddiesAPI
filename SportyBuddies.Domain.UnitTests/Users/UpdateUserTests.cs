using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestConstants;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class UpdateUserTests
{
    [Fact]
    public void Update_Should_SetPropertyValue()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        // Act
        user.Update(UserConstants.Name, UserConstants.Description, UserConstants.DateOfBirth, UserConstants.Gender);

        // Assert
        user.Name.Should().Be(UserConstants.Name);
        user.Description.Should().Be(UserConstants.Description);
        user.DateOfBirth.Should().Be(UserConstants.DateOfBirth);
        user.Gender.Should().Be(UserConstants.Gender);
    }
}