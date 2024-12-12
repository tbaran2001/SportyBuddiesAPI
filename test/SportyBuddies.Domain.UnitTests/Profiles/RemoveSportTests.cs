using FluentAssertions;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Profiles.Events;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.Infrastructure;
using SportyBuddies.Domain.UnitTests.TestData;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class RemoveSportTests:BaseTest
{
    [Fact]
    public void RemoveSport_WhenUserHasSport_ShouldRemoveSport()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
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
        var user = Profile.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);

        // Act
        var act = () => user.RemoveSport(sport);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Profile does not have this sport");
    }

    [Fact]
    public void RemoveSport_Should_RaiseSportRemovedDomainEvent()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        var sport = Sport.Create(SportData.Name, SportData.Description);
        user.AddSport(sport);

        // Act
        user.RemoveSport(sport);

        // Assert
        var domainEvent = AssertDomainEventWasPublished<ProfileSportRemovedEvent>(user);

        domainEvent.ProfileId.Should().Be(user.Id);
        domainEvent.SportId.Should().Be(sport.Id);
    }
}