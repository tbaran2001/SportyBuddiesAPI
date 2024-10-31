using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestData;
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
        user.Update(UserData.Name, UserData.Description, UserData.DateOfBirth, UserData.Gender);

        // Assert
        user.Name.Should().Be(UserData.Name);
        user.Description.Should().Be(UserData.Description);
        user.DateOfBirth.Should().Be(UserData.DateOfBirth);
        user.Gender.Should().Be(UserData.Gender);
    }
}