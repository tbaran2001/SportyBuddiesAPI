using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestUtils.Sports;
using SportyBuddies.Domain.UnitTests.TestUtils.Users;

namespace SportyBuddies.Domain.UnitTests.UserTests;

public class RemoveAllSportsTests
{
    [Fact]
    public void RemoveAllSports_WhenUserHasSports_ShouldRemoveAllSports()
    {
        // Arrange
        var user = UserFactory.Create();
        var sport1 = SportFactory.Create(id: Guid.NewGuid());
        var sport2 = SportFactory.Create();
        user.AddSport(sport1);
        user.AddSport(sport2);

        // Act
        user.RemoveAllSports();

        // Assert
        user.Sports.Should().BeEmpty();
    }
}