using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.Infrastructure;
using SportyBuddies.Domain.UnitTests.TestData;
using SportyBuddies.Domain.Users;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Domain.UnitTests.Users;

public class AddSportTests:BaseTest
{
    [Fact]
    public void AddSport_WhenUserDoesNotHaveSport_ShouldAddSport()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);

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
        var sport = Sport.Create(SportData.Name, SportData.Description);
        user.AddSport(sport);

        // Act
        var act = () => user.AddSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("User already has this sport");
    }

    [Fact]
    public void AddSport_Should_RaiseSportAddedDomainEvent()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);

        // Act
        user.AddSport(sport);

        // Assert
        var domainEvent = AssertDomainEventWasPublished<UserSportAddedEvent>(user);

        domainEvent.UserId.Should().Be(user.Id);
        domainEvent.SportId.Should().Be(sport.Id);
    }
}