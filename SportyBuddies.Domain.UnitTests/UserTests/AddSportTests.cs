using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestUtils.Sports;
using SportyBuddies.Domain.UnitTests.TestUtils.Users;

namespace SportyBuddies.Domain.UnitTests.UserTests;

public class AddSportTests
{
    [Fact]
    public void AddSport_WhenUserDoesNotHaveSport_ShouldAddSport()
    {
        // Arrange
        var user = UserFactory.Create();
        var sport = SportFactory.Create();

        // Act
        user.AddSport(sport);

        // Assert
        user.Sports.Should().Contain(sport);
    }

    [Fact]
    public void AddSport_WhenUserHasSport_ShouldThrowException()
    {
        // Arrange
        var user = UserFactory.Create();
        var sport = SportFactory.Create();
        user.AddSport(sport);

        // Act
        var act = () => user.AddSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("User already has this sport");

        //cleanup
        user.RemoveSport(sport);
    }
}