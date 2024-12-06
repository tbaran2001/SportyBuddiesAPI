using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.Infrastructure;
using SportyBuddies.Domain.UnitTests.TestData;
using SportyBuddies.Domain.Users;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Domain.UnitTests.Users;

public class RemoveSportTests:BaseTest
{
    [Fact]
    public void RemoveSport_WhenUserHasSport_ShouldRemoveSport()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);
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
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);

        // Act
        var act = () => user.RemoveSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("User does not have this sport");
    }

    [Fact]
    public void RemoveSport_Should_RaiseSportRemovedDomainEvent()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);
        user.AddSport(sport);

        // Act
        user.RemoveSport(sport);

        // Assert
        var domainEvent = AssertDomainEventWasPublished<UserSportRemovedEvent>(user);

        domainEvent.UserId.Should().Be(user.Id);
        domainEvent.SportId.Should().Be(sport.Id);
    }
}