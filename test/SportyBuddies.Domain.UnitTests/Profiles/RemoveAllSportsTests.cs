using FluentAssertions;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestData;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class RemoveAllSportsTests
{
    [Fact]
    public void RemoveAllSports_WhenUserHasSports_ShouldRemoveAllSports()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
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