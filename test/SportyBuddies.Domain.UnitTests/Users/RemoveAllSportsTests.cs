using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestData;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class RemoveAllSportsTests
{
    [Fact]
    public void RemoveAllSports_WhenUserHasSports_ShouldRemoveAllSports()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport1 = Sport.Create(SportData.Name, SportData.Description);
        var sport2 = Sport.Create(SportData.Name, SportData.Description);
        user.AddSport(sport1);
        user.AddSport(sport2);

        // Act
        user.RemoveAllSports();

        // Assert
        user.Sports.Should().BeEmpty();
    }
}