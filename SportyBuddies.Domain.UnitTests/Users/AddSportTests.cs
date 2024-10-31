using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestConstants;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class AddSportTests
{
    [Fact]
    public void AddSport_WhenUserDoesNotHaveSport_ShouldAddSport()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportConstants.Name, SportConstants.Description);

        // Act
        user.AddSport(sport);

        // Assert
        user.Sports.Should().Contain(sport);
    }

    [Fact]
    public void AddSport_WhenUserHasSport_ShouldThrowException()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportConstants.Name, SportConstants.Description);
        user.AddSport(sport);

        // Act
        var act = () => user.AddSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("User already has this sport");
    }
}