using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.UserSports.Commands;

public class RemoveUserSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task RemoveUserSport_ShouldRemoveSportFromUser()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        user.AddSport(sport);
        await DbContext.Users.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveUserSportCommand(sport.Id);

        // Act
        await Sender.Send(command);

        // Assert
        user.Sports.Should().BeEmpty();
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowException_WhenUserDoesNotHaveSport()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Users.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveUserSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User does not have this sport");
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();

        var command = new RemoveUserSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task RemoveUserSport_ShouldThrowNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = new RemoveUserSportCommand(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}