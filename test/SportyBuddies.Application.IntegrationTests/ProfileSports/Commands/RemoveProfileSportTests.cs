using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.ProfileSports.Commands.RemoveProfileSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.ProfileSports.Commands;

public class RemoveProfileSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task RemoveUserSport_ShouldRemoveSportFromUser()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        user.AddSport(sport);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(sport.Id);

        // Act
        await Sender.Send(command);

        // Assert
        user.Sports.Should().BeEmpty();
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowException_WhenUserDoesNotHaveSport()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Profiles.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Profile does not have this sport");
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}