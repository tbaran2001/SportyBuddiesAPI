using FluentAssertions;
using SportyBuddies.Domain.UnitTests.TestUtils.Sports;
using SportyBuddies.Domain.UnitTests.TestUtils.Users;

namespace SportyBuddies.Domain.UnitTests.UserTests;

public class RemoveSportTests
{
    [Fact]
    public void RemoveSport_WhenUserHasSport_ShouldRemoveSport()
    {
        // Arrange
        var user = UserFactory.Create();
        var sport = SportFactory.Create();
        user.AddSport(sport);

        // Act
        user.RemoveSport(sport);

        // Assert
        user.Sports.Should().NotContain(sport);
    }

    [Fact]
    public void RemoveSport_WhenUserDoesNotHaveSport_ShouldThrowException()
    {
        // Arrange
        var user = UserFactory.Create();
        var sport = SportFactory.Create();

        // Act
        var act = () => user.RemoveSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("User does not have this sport");
    }
}